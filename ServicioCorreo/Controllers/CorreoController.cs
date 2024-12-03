using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicioCorreo.Aplicacion.Funcionalidad.Correos.Comando.EnviarCorreos;
using System.Net;

namespace ServicioCorreo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorreoController : ControllerBase
    {
        private IMediator _mediador;
        public CorreoController(IMediator mediador)
        {
             _mediador = mediador;
        }

        [HttpPost("Enviar", Name = "EnviarCorreo")]
        [ProducesResponseType(typeof(bool), (int)(HttpStatusCode.OK))]
        public async Task<bool> Enviar([FromBody] EnviarCorreosComando correo)
        {
            return await _mediador.Send(correo);
        }
    }
}
