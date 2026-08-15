using MassTransit;
using MediatR;
using NeoCore.Domain.EventosDominio.LibroContable;
using NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.TransferenciaEventos;

namespace NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.Publicadores
{
    public class AcreditarSaldoExitosoPublicador : INotificationHandler<AcreditarDinero>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public AcreditarSaldoExitosoPublicador(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(AcreditarDinero notification, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new AcreditacionSaldoExitoso
            {
                TransferenciaId = notification.IdCorrelacion
            }, cancellationToken);
        }
    }
}
