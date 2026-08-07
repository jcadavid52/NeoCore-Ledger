using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeoCore.Application.CasosDeUso.Transferencias.Comandos;
using NeoCore.Application.CasosDeUso.Transferencias.Manejadores;
using NeoCore.Infrastructure.EntryPoint.Rest.Filtros;

namespace NeoCore.Infrastructure.EntryPoint.Rest.Controllers
{
    [ApiController]
    [Route("v1/transferencias")]
    [Produces("application/json")]
    public class TransferenciaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransferenciaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("iniciar")]
        [ServiceFilter(typeof(FiltroIdempotencia))]
        public async Task<IActionResult> TransferirDinero([FromBody] IniciarTransferenciaComando comando, CancellationToken cancellationToken)
        {
            var respuesta = await _mediator.Send(comando, cancellationToken);
            return Accepted(respuesta);
        }
    }
}
