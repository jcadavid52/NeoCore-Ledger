using MassTransit;
using MediatR;
using NeoCore.Domain.EventosDominio.LibroContable;
using NeoCore.Infrastructure.OutputPoint.Sagas.TransferenciaEventos;

namespace NeoCore.Infrastructure.OutputPoint.Sagas.Publicadores
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
            await _publishEndpoint.Publish(new AcreditacionExitosa
            {
                TransferenciaId = notification.IdCorrelacion
            }, cancellationToken);
        }
    }
}
