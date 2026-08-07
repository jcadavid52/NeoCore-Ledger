using MassTransit;
using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Infrastructure.OutputPoint.Sagas.Comandos;
using NeoCore.SharedKernel.LibroContable;


namespace NeoCore.Infrastructure.OutputPoint.Sagas.Consumidores
{
    public class LiquidarSaldoConsumer : IConsumer<LiquidarSaldoCommandSaga>
    {
        private readonly IMediator _mediator;

        public LiquidarSaldoConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<LiquidarSaldoCommandSaga> context)
        {
            var msg = context.Message;

            var command = new LiquidarSaldoComando(
                msg.IdCuentaOrigen,
                msg.Monto,
                msg.IdCorrelacion,
                TipoCorrelacionEnum.Transferencia);

            await _mediator.Send(command, context.CancellationToken);
        }
    }
}
