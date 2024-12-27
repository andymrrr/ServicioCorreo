using System.Net.Mail;
using System.Net;
using ServicioCorreo.Servicios.Modelo;
using Microsoft.Extensions.Options;
using ServicioCorreo.Servicios.Interffaz;
using Microsoft.Extensions.Configuration;

namespace ServicioCorreo.Servicios.Repositorios
{
    public class ServicioCorreos : IServicioCorreos
    {
        private readonly ConfiguracionCorreo _config;

        public ServicioCorreos(IOptions<ConfiguracionCorreo> config)
        {
            _config = config.Value;
        }

        public async Task<bool> EnviarCorreoAsync(
            string destinatario,
            string asunto,
            string cuerpoHtml,
            MailPriority prioridad = MailPriority.Normal,
            IEnumerable<string>? rutasAdjuntos = null)
        {
            try
            {
                // Crear el cliente SMTP
                using (var smtpClient = new SmtpClient(_config.ServidorSmtp, _config.Puerto))
                {
                    // Configuración del cliente SMTP
                    smtpClient.Credentials = new NetworkCredential(_config.CorreoRemitente, _config.Contraseña);
                    smtpClient.EnableSsl = _config.UsarSsl;

                    // Crear el correo
                    var correo = new MailMessage
                    {
                        From = new MailAddress(_config.CorreoRemitente, _config.NombreRemitente),
                        Subject = asunto,
                        Body = cuerpoHtml,
                        IsBodyHtml = true,
                        Priority = prioridad
                    };

                    // Agregar el destinatario
                    correo.To.Add(destinatario);

                    // Agregar los adjuntos si se proporcionan
                    if (rutasAdjuntos != null)
                    {
                        foreach (var ruta in rutasAdjuntos)
                        {
                            if (!string.IsNullOrEmpty(ruta))
                            {
                                correo.Attachments.Add(new Attachment(ruta));
                            }
                        }
                    }

                    // Enviar el correo de manera asíncrona
                    await smtpClient.SendMailAsync(correo);

                    // Si todo salió bien, retornar true
                    return true;
                }
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
    }
}
