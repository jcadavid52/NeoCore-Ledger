using MassTransit;
using MediatR;
using NeoCore.Domain.EventosDominio.LibroContable;

namespace NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.Publicadores
{
    public class LiquidarSaldoExistosoPublicador : INotificationHandler<DebitarDinero>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public LiquidarSaldoExistosoPublicador(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(DebitarDinero notification, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new LiquidacionExitosa
            {
                TransferenciaId = notification.IdCorrelacion
            }, cancellationToken);
        }
    }
}
