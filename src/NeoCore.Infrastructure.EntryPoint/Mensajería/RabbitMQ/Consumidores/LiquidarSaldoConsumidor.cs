using MassTransit;
using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Consumidores.Comandos;
using NeoCore.SharedKernel.LibroContable;


namespace NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Consumidores
{
    public class LiquidarSaldoConsumidor : IConsumer<LiquidarSaldoComandoConsumidor>
    {
        private readonly IMediator _mediator;

        public LiquidarSaldoConsumidor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<LiquidarSaldoComandoConsumidor> context)
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
