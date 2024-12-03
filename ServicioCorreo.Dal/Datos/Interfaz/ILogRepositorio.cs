using ServicioCorreo.Dal.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioCorreo.Dal.Datos.Interfaz
{
    public interface ILogRepositorio
    {
        Task RegistrarLogAsync(Log log);
        Task<IEnumerable<Log>> ObtenerLogsAsync();
        Task<Log> ObtenerLogPorIdAsync(int id);
    }
}
