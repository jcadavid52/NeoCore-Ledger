using MassTransit;
using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Infrastructure.OutputPoint.Sagas.Comandos;
using NeoCore.Infrastructure.OutputPoint.Sagas.TransferenciaEventos;
using NeoCore.SharedKernel.LibroContable;


namespace NeoCore.Infrastructure.OutputPoint.Sagas.Consumidores
{
    public class LiquidarSaldoConsumer : IConsumer<LiquidarSaldoCommandSaga>
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public LiquidarSaldoConsumer(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<LiquidarSaldoCommandSaga> context)
        {
            var msg = context.Message;

            var command = new LiquidarSaldoComando(
                msg.IdCuentaOrigen,
                msg.Monto,
                msg.IdCorrelacion,
                TipoCorrelacionEnum.Transferencia);

            var resultado = await _mediator.Send(command, context.CancellationToken);

            if (resultado.Exitoso)
            {
                await _publishEndpoint.Publish(
                    new LiquidacionExitosa
                    {
                        TransferenciaId = msg.IdCorrelacion
                    });
            }
        }
    }
}
