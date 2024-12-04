
using ServicioCorreo.Dal.Datos.Paginacion;
using ServicioCorreo.Dal.Modelo;
using System.Linq.Expressions;

namespace ServicioCorreo.Aplicacion.Paginacion.Plantillas
{
    public class PaginacionPalntillaParametro : PaginacionParametro
    {
        public int? PlantillaId { get; set; }
        public string? Nombre { get; set; }

        // Método para construir el filtro de búsqueda
        public Expression<Func<Plantilla, bool>> ConstruirFiltro()
        {
            return p =>
                (!PlantillaId.HasValue || p.Id == PlantillaId) &&
                (string.IsNullOrEmpty(Nombre) || p.Nombre!.Contains(Nombre)) &&
                (string.IsNullOrEmpty(Busqueda) || p.Nombre!.ToLower().Contains(Busqueda.ToLower()));
        }
    }
}
