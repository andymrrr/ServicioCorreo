using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicioCorreo.Aplicacion.Funcionalidad.Correos.Comando.EnviarCorreos;
using ServicioCorreo.Aplicacion.Funcionalidad.Plantilla.Consultas.ObtenerPlantillasConParametros;
using ServicioCorreo.Aplicacion.Funcionalidad.Plantilla.Vm;
using System.Net;

namespace ServicioCorreo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantillaController : ControllerBase
    {
        private IMediator _mediador;
        public PlantillaController(IMediator mediador)
        {
            _mediador = mediador;
        }

        [HttpGet("ObtenerPlantillaConParametro", Name = "ObtenerPlantillaConParametro")]
        [ProducesResponseType(typeof(List<PlantillaVm>), (int)(HttpStatusCode.OK))]
        public async Task<List<PlantillaVm>> ObtenerPlantillaConParametro()
        {
            var plantilla = new ObtenerPlantillasConParametrosConsulta();
            return await _mediador.Send(plantilla);
        }

    }
}
