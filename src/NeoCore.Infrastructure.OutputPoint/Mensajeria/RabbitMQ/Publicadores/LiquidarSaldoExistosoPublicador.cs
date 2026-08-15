using MassTransit;
using MediatR;
using NeoCore.Domain.EventosDominio.LibroContable;
using NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.TransferenciaEventos;

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
            await _publishEndpoint.Publish(new LiquidacionSaldoExitoso
            {
                TransferenciaId = notification.IdCorrelacion
            }, cancellationToken);
        }
    }
}
