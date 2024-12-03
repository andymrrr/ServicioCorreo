using Microsoft.EntityFrameworkCore;
using ServicioCorreo.Dal.Datos.Context;
using ServicioCorreo.Dal.Datos.Interfaz;
using ServicioCorreo.Dal.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioCorreo.Dal.Datos.Repositorio
{
    public class LogRepositorio : ILogRepositorio
    {
        private readonly ContextCorreo _context;

        public LogRepositorio(ContextCorreo context)
        {
            _context = context;
        }

        public async Task RegistrarLogAsync(Log log)
        {
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Log>> ObtenerLogsAsync()
        {
            return await _context.Logs.ToListAsync();
        }

        public async Task<Log> ObtenerLogPorIdAsync(int id)
        {
            return await _context.Logs.FindAsync(id);
        }
    }
}
