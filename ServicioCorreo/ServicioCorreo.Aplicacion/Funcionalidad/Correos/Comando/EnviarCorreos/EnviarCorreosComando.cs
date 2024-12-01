using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioCorreo.Aplicacion.Funcionalidad.Correos.Comando.EnviarCorreos
{
    public class EnviarCorreosComando: IRequest<bool>
    {
        public string Destinatario { get; set; }
        public string Asunto { get; set; }
        public int? IdPlantilla { get; set; } 
        public string? CuerpoHtmlPersonalizado { get; set; }

        public Dictionary<string, string> Parametros { get; set; } = new Dictionary<string, string>();
    }
}
