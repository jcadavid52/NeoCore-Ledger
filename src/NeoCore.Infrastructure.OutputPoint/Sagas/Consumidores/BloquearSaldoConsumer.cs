using MassTransit;
using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Infrastructure.OutputPoint.Sagas.Comandos;
using NeoCore.SharedKernel.LibroContable;

namespace NeoCore.Infrastructure.OutputPoint.Sagas.Consumidores
{
    public class BloquearSaldoConsumer : IConsumer<BloquearSaldoCommandSaga>
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public BloquearSaldoConsumer(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
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
