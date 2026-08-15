using MassTransit;
using MediatR;
using NeoCore.Domain.EventosDominio.LibroContable;
using NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.TransferenciaEventos;

namespace NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.Publicadores
{
    public class BloquearSaldoExistosoPublicador : INotificationHandler<BloquearSaldo>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public BloquearSaldoExistosoPublicador(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(BloquearSaldo notification, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new SaldoBloqueadoExitoso
            {
                TransferenciaId = notification.IdCorrelacion
            }, cancellationToken);
        }
    }
}
