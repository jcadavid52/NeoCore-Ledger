using Microsoft.AspNetCore.Mvc;
using MediatR;
using NeoCore.Application.Transacciones.Comandos;

namespace NeoCore.Infrastructure.EntryPoint.Rest.Controllers
{
    [ApiController]
    [Route("v1/transacciones")]
    [Produces("application/json")]
    public class TransaccionController: ControllerBase
    {
        private readonly IMediator _mediator;

        public TransaccionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("retirar-dinero")]
        public async Task<IActionResult> RetirarDinero([FromBody] RetirarDineroComando comando, CancellationToken cancellationToken)
        {
            await _mediator.Send(comando, cancellationToken);

            return Ok(new { message = "Dinero retirado exitosamente." });
        }
    }
}
