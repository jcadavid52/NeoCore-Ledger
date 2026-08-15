using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Domain.Repositorios;
using NeoCore.Infrastructure.EntryPoint.Rest.Filtros;

namespace NeoCore.Infrastructure.EntryPoint.Rest.Controllers
{
    [ApiController]
    [Route("v1/libro-contable")]
    [Produces("application/json")]
    public class LibroContableController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILibroContableRepositorio _cuentaContableRepositorio;

        public LibroContableController(
            IMediator mediator,
            ILibroContableRepositorio cuentaContableRepositorio)
        {
            _mediator = mediator;
            _cuentaContableRepositorio = cuentaContableRepositorio;
        }

        [HttpGet("ping/{id}")]
        public async Task<IActionResult> Ping(Guid id, CancellationToken cancellationToken)
        {
            var cuenta = await _cuentaContableRepositorio.CargarAsync(id, cancellationToken);
            return Ok(new { message = "Pong", saldo = cuenta.SaldoDisponible });
        }

        [HttpPost("retirar-dinero")]
        [ServiceFilter(typeof(FiltroIdempotencia))]
        public async Task<IActionResult> RetirarDinero([FromBody] RetirarDineroComando comando, CancellationToken cancellationToken)
        {
            await _mediator.Send(comando, cancellationToken);

            return Ok(new { message = "Dinero retirado exitosamente." });
        }
    }
}
