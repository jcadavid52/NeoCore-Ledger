using MassTransit;
using MediatR;
using NeoCore.Application.CasosDeUso.Transferencias.Comandos;
using NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Consumidores.Comandos;

namespace NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Consumidores
{
    public class NotificarTransferenciaConsumidor : IConsumer<NotificarTransferenciaComandoConsumidor>
    {
        private readonly IMediator _mediator;

        public NotificarTransferenciaConsumidor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<NotificarTransferenciaComandoConsumidor> context)
        {
            var msg = context.Message;

            var command = new NotificarTransferenciaComando(msg.IdCorrelacion);

            await _mediator.Send(command, context.CancellationToken);
        }
    }
}
