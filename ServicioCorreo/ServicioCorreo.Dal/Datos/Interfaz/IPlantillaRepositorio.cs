

using ServicioCorreo.Dal.Modelo;

namespace ServicioCorreo.Dal.Datos.Interfaz
{
    public interface IPlantillaRepositorio
    {
        Task CrearPlantillaAsync(Plantilla plantilla);
        Task<IEnumerable<Plantilla>> ObtenerPlantillasAsync();
        Task<Plantilla> ObtenerPlantillaPorIdAsync(int id);
        Task ActualizarPlantillaAsync(Plantilla  plantilla);
    }
}
