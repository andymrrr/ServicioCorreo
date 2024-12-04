
using MediatR;
using ServicioCorreo.Aplicacion.Funcionalidad.Plantilla.Vm;
using ServicioCorreo.Dal.Datos.Paginacion;
using ServicioCorreo.Dal.Datos.Paginacion.Modelo;


namespace ServicioCorreo.Aplicacion.Funcionalidad.Plantilla.Consultas.PaginacionPlantillas
{
    public class PaginacionPlantillasConsulta : PaginacionParametro, IRequest<PaginacionVm<PlantillaVm>>
    {
        public int? PlantillaId { get; set; }
        public string? Nombre { get; set; }
    }

}
