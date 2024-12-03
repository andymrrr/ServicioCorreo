
using Microsoft.EntityFrameworkCore;
using ServicioCorreo.Dal.Datos.Context;
using ServicioCorreo.Dal.Modelo;

namespace ServicioCorreo.Dal.Datos.Repositorio
{
    public class PlantillaRepositorio
    {
        private readonly ContextCorreo _context;

        public PlantillaRepositorio(ContextCorreo context)
        {
            _context = context;
        }

        public async Task CrearPlantillaAsync(Plantilla plantilla)
        {
            _context.Plantillas.Add(plantilla);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Plantilla>> ObtenerPlantillasAsync()
        {
            return await _context.Plantillas.ToListAsync();
        }

        public async Task<Plantilla> ObtenerPlantillaPorIdAsync(int id)
        {
            return await _context.Plantillas.FindAsync(id);
        }

        public async Task ActualizarPlantillaAsync(Plantilla plantilla)
        {
            _context.Plantillas.Update(plantilla);
            await _context.SaveChangesAsync();
        }
    }
}
