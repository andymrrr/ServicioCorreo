

using ServicioCorreo.Dal.Datos.Paginacion;
using ServicioCorreo.Dal.Modelo;

namespace ServicioCorreo.Aplicacion.Paginacion.Plantillas
{
    public class PaginacionParaConteoDePalntilla : PaginacionBase<Plantilla>
    {
        public PaginacionParaConteoDePalntilla(PaginacionPalntillaParametro parametro)
         : base(parametro.ConstruirFiltro())
        { }
    }
}
