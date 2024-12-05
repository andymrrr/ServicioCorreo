using MediatR;
using ServicioCorreo.Aplicacion.Funcionalidad.Correos.Comando.EnviarCorreos;
using ServicioCorreo.Dal.Datos.Context;
using ServicioCorreo.Dal.Datos.Interfaz;
using ServicioCorreo.Dal.Modelo;
using ServicioCorreo.Dal.Utilitario.Correo;
using ServicioCorreo.Servicios.Interffaz;
using System.Text.RegularExpressions;


public class EnviarCorreosComandoHandler : IRequestHandler<EnviarCorreosComando, bool>
{
    private readonly IServicioCorreos _servicioCorreos;
    private readonly ICorreoUoW _context;
    private readonly PlantillaProcesador _plantillaProcesador;

    public EnviarCorreosComandoHandler(IServicioCorreos servicioCorreos, ICorreoUoW context, PlantillaProcesador plantillaProcesador)
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
            var plantilla = await _context.Plantilla.BuscarPorIdAsincrono(request.IdPlantilla!.Value!);

            if (plantilla is null)
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

        if (request.Parametros != null)
        {
            cuerpoHtml = _plantillaProcesador.ReemplazarParametros(cuerpoHtml, request.Parametros);
        }

        
        if (request.Items != null && request.Items.Any())
        {
            // Detectar y extraer la sección {{#Items}} ... {{/Items}}
            var itemsPattern = @"{{#Items}}(.*?){{/Items}}";
            var itemsMatch = Regex.Match(cuerpoHtml, itemsPattern, RegexOptions.Singleline);

            if (itemsMatch.Success)
            {
                var itemTemplate = itemsMatch.Groups[1].Value;
                var renderedItems = request.Items.Select(item =>
                {
                    var itemStringDictionary = item.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.ToString() ?? string.Empty
                    );

                    return _plantillaProcesador.ReemplazarParametros(itemTemplate, itemStringDictionary);
                });

                cuerpoHtml = cuerpoHtml.Replace(itemsMatch.Value, string.Join("", renderedItems));
            }
        }
        else
        {
            
            var itemsPattern = @"{{#Items}}(.*?){{/Items}}";
            cuerpoHtml = Regex.Replace(cuerpoHtml, itemsPattern, string.Empty,RegexOptions.Singleline);
        }

        try
        {
            await _servicioCorreos.EnviarCorreoAsync(request.Destinatario, request.Asunto, cuerpoHtml,request.Prioridad,request.Adjuntos);
            var log = new Log
            {
                Asunto = request.Asunto,
                Destinatario = request.Destinatario,
                Error = "N/A",
                Exito = true,
                FechaEnvio = DateTime.Now,
            };
            await _context.Log.AgregarAsincrono(log);

            await _context.GuardarCambiosAsync();
            return true;
        }
        catch (Exception ex)
        {
            var log = new Log
            {
                Asunto = request.Asunto,
                Destinatario = request.Destinatario,
                Error = ex.Message,
                Exito = true,
                FechaEnvio = DateTime.Now,
            };
            await _context.Log.AgregarAsincrono(log);

            await _context.GuardarCambiosAsync();
            return false;
        }
    }

}
