using MassTransit;
using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Comandos;
using NeoCore.SharedKernel.LibroContable;

namespace NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Consumidores
{
    public class BloquearSaldoConsumer : IConsumer<BloquearSaldoCommandSaga>
    {
        private readonly IMediator _mediator;

        public BloquearSaldoConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<BloquearSaldoCommandSaga> context)
        {
            var msg = context.Message;

            var command = new BloquearSaldoComando(
                msg.IdCuentaOrigen,
                msg.Monto,
                msg.IdCorrelacion,
                TipoCorrelacionEnum.Transferencia);

            await _mediator.Send(command, context.CancellationToken);
        }
    }
}
