using MediatR;
using ServicioCorreo.Dal.Datos.Context;
using ServicioCorreo.Dal.Utilitario.Correo;
using ServicioCorreo.Servicios.Interffaz;

namespace ServicioCorreo.Aplicacion.Funcionalidad.Correos.Comando.EnviarCorreos
{

    namespace ServicioCorreo.Aplicacion.Funcionalidad.Correos.Comando.EnviarCorreos
    {
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

                // Reemplazar parámetros en el cuerpo HTML
                if (request.Parametros != null)
                {
                    cuerpoHtml = _plantillaProcesador.ReemplazarParametros(cuerpoHtml, request.Parametros);
                }

                try
                {
                    await _servicioCorreos.EnviarCorreoAsync(request.Destinatario, request.Asunto, cuerpoHtml);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }

}
