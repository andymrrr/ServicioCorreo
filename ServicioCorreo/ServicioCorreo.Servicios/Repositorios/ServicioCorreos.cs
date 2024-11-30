using System.Net.Mail;
using System.Net;
using ServicioCorreo.Servicios.Modelo;
using Microsoft.Extensions.Options;
using ServicioCorreo.Servicios.Interffaz;

namespace ServicioCorreo.Servicios.Repositorios
{
    public class ServicioCorreos  : IServicioCorreos
    {
        private readonly ConfiguracionCorreo _config;

        public ServicioCorreos(IOptions<ConfiguracionCorreo> config)
        {
            _config = config.Value;
        }

        public async Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpoHtml)
        {
            using (var smtpClient = new SmtpClient(_config.ServidorSmtp, _config.Puerto))
            {
                smtpClient.Credentials = new NetworkCredential(_config.CorreoRemitente, _config.Contraseña);
                smtpClient.EnableSsl = _config.UsarSsl;

                var correo = new MailMessage
                {
                    From = new MailAddress(_config.CorreoRemitente, _config.NombreRemitente),
                    Subject = asunto,
                    Body = cuerpoHtml,
                    IsBodyHtml = true // Indica que el cuerpo es HTML
                };

                correo.To.Add(destinatario);

                try
                {
                    await smtpClient.SendMailAsync(correo);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error al enviar el correo", ex);
                }
            }
        }
    }
}
