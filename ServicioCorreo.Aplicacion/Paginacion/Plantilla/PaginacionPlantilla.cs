


using ServicioCorreo.Dal.Datos.Paginacion;
using ServicioCorreo.Dal.Modelo;

namespace ServicioCorreo.Aplicacion.Paginacion.Plantillas
{
    public class PaginacionPlantilla : PaginacionBase<Plantilla>
    {
        public PaginacionPlantilla(PaginacionPalntillaParametro parametro) : base(parametro.ConstruirFiltro())
        {
            // Incluir relaciones necesarias
           // AgregarIncluir(c => c.Subcategorias!);
           // AgregarIncluir(c => c.Artes!);

            // Configurar paginación
            AplicarPaginacion(parametro.Pagina, parametro.CantidadRegistroPorPagina);

            // Aplicar ordenamiento
            AplicarOrdenamiento(parametro.Ordenar);
        }

        private void AplicarOrdenamiento(string? orden)
        {
            if (string.IsNullOrEmpty(orden))
            {
                AgregarOrdenarPor(c => c.Id); // Ordenar por ID por defecto
                return;
            }

            // Aplicar ordenamiento basado en el parámetro
            var ordenamiento = orden.ToLower();
            if (ordenamiento == "nombreasc")
            {
                AgregarOrdenarPor(c => c.Nombre!);
            }
            else if (ordenamiento == "nombredesc")
            {
                AgregarOrdenarDescendiente(c => c.Nombre!);
            }
            else
            {
                AgregarOrdenarPor(c => c.Id); // Ordenar por ID si no coincide con ningún caso
            }
        }
    }


}
