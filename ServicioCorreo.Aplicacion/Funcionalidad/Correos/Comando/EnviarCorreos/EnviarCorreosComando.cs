using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ServicioCorreo.Aplicacion.Funcionalidad.Correos.Comando.EnviarCorreos
{
    public class EnviarCorreosComando : IRequest<bool>
    {
        public string Destinatario { get; set; } = string.Empty;
        public string Asunto { get; set; } = string.Empty;
        public int? IdPlantilla { get; set; }
        public string? CuerpoHtmlPersonalizado { get; set; }
        public Dictionary<string, string> Parametros { get; set; } = new Dictionary<string, string>();
        public List<Dictionary<string, object>>? Items { get; set; } = new List<Dictionary<string, object>>();
        public MailPriority Prioridad { get; set; } = MailPriority.Normal;
        public List<string>? Adjuntos { get; set; } = new List<string>();
    }


}
