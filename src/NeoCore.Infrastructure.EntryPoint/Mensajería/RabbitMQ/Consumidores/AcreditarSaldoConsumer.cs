using MassTransit;
using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Comandos;
using NeoCore.SharedKernel.LibroContable;


namespace NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Consumidores
{
    public class AcreditarSaldoConsumer : IConsumer<AcreditarSaldoCommandSaga>
    {
        private readonly IMediator _mediator;

        public AcreditarSaldoConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<AcreditarSaldoCommandSaga> context)
        {
            var msg = context.Message;

            var command = new AcreditarSaldoComando(
                msg.IdCuentaDestino,
                msg.Monto,
                msg.IdCorrelacion,
                TipoCorrelacionEnum.Transferencia);

            await _mediator.Send(command, context.CancellationToken);
        }
    }
}
