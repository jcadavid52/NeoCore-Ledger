using MassTransit;
using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Infrastructure.OutputPoint.Sagas.Comandos;
using NeoCore.Infrastructure.OutputPoint.Sagas.TransferenciaEventos;
using NeoCore.SharedKernel.LibroContable;


namespace NeoCore.Infrastructure.OutputPoint.Sagas.Consumidores
{
    public class AcreditarSaldoConsumer : IConsumer<AcreditarSaldoCommandSaga>
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public AcreditarSaldoConsumer(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
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
