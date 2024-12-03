

using MediatR;
using ServicioCorreo.Aplicacion.Funcionalidad.Plantilla.Vm;

namespace ServicioCorreo.Aplicacion.Funcionalidad.Plantilla.Consultas.ObtenerPlantillasConParametros
{
    public class ObtenerPlantillasConParametrosConsulta: IRequest<List<PlantillaVm>>
    {
    }
}
