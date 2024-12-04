using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicioCorreo.Aplicacion.Funcionalidad.Plantilla.Consultas.PaginacionPlantillas;
using ServicioCorreo.Aplicacion.Funcionalidad.Plantilla.Vm;
using ServicioCorreo.Dal.Datos.Paginacion.Modelo;
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

       
        [HttpGet("paginacion", Name = "PaginacionPlantilla")]
        [ProducesResponseType(typeof(PaginacionVm<PlantillaVm>), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<PaginacionVm<PlantillaVm>>> PaginacionPlantilla([FromQuery] PaginacionPlantillasConsulta consulta)
        {

            var paginacion = await _mediador.Send(consulta);
            return Ok(paginacion);
        }
    }
}
