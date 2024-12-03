using MediatR;
using ServicioCorreo.Aplicacion.Funcionalidad.Correos.Comando.EnviarCorreos;
using ServicioCorreo.Dal.Datos.Context;
using ServicioCorreo.Dal.Utilitario.Correo;
using ServicioCorreo.Servicios.Interffaz;


public class EnviarCorreosComandoHandler : IRequestHandler<EnviarCorreosComando, bool>
{
    private readonly IServicioCorreos _servicioCorreos;
    private readonly ContextCorreo _context;
    private readonly PlantillaProcesador _plantillaProcesador;

    public EnviarCorreosComandoHandler(IServicioCorreos servicioCorreos, ContextCorreo context, PlantillaProcesador plantillaProcesador)
    {
        _servicioCorreos = servicioCorreos;
        _context = context;
        _plantillaProcesador = plantillaProcesador;
    }

    public async Task<bool> Handle(EnviarCorreosComando request, CancellationToken cancellationToken)
    {
        string cuerpoHtml;

        // Obtener la plantilla o el contenido personalizado
        if (request.IdPlantilla.HasValue)
        {
            var plantilla = await _context.Plantillas.FindAsync(request.IdPlantilla.Value);

            if (plantilla == null)
            {
                throw new KeyNotFoundException($"La plantilla con ID {request.IdPlantilla} no fue encontrada.");
            }

            cuerpoHtml = plantilla.ContenidoHtml;
        }
        else if (!string.IsNullOrEmpty(request.CuerpoHtmlPersonalizado))
        {
            cuerpoHtml = request.CuerpoHtmlPersonalizado;
        }
        else
        {
            throw new ArgumentException("Debe especificar una plantilla o un cuerpo HTML personalizado.");
        }

        // Reemplazar parámetros generales
        if (request.Parametros != null)
        {
            cuerpoHtml = _plantillaProcesador.ReemplazarParametros(cuerpoHtml, request.Parametros);
        }

        // Procesar {{#Items}} ... {{/Items}}
        if (request.Items != null && request.Items.Any())
        {
            // Detectar y extraer la sección {{#Items}} ... {{/Items}}
            var itemsPattern = @"{{#Items}}(.*?){{/Items}}";
            var itemsMatch = System.Text.RegularExpressions.Regex.Match(cuerpoHtml, itemsPattern, System.Text.RegularExpressions.RegexOptions.Singleline);

            if (itemsMatch.Success)
            {
                var itemTemplate = itemsMatch.Groups[1].Value;
                var renderedItems = request.Items.Select(item =>
                {
                    // Convertir el Dictionary<string, object> en Dictionary<string, string>
                    var itemStringDictionary = item.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.ToString() ?? string.Empty
                    );

                    // Reemplazar los parámetros del template del item
                    return _plantillaProcesador.ReemplazarParametros(itemTemplate, itemStringDictionary);
                });

                // Reemplazar el bloque {{#Items}} ... {{/Items}} en el HTML final
                cuerpoHtml = cuerpoHtml.Replace(itemsMatch.Value, string.Join("", renderedItems));
            }
        }
        else
        {
            // Si no hay items, eliminar la sección {{#Items}} ... {{/Items}}
            var itemsPattern = @"{{#Items}}(.*?){{/Items}}";
            cuerpoHtml = System.Text.RegularExpressions.Regex.Replace(cuerpoHtml, itemsPattern, string.Empty, System.Text.RegularExpressions.RegexOptions.Singleline);
        }

        try
        {
            await _servicioCorreos.EnviarCorreoAsync(request.Destinatario, request.Asunto, cuerpoHtml);
            return true;
        }
        catch (Exception ex)
        {
            // Log del error
            Console.WriteLine($"Error al enviar correo: {ex.Message}");
            return false;
        }
    }

}
