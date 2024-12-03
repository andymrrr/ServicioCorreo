using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioCorreo.Servicios.Interffaz
{
    public interface IServicioCorreos
    {
        Task<bool> EnviarCorreoAsync(string destinatario, string asunto, string cuerpoHtml);
    }
}
