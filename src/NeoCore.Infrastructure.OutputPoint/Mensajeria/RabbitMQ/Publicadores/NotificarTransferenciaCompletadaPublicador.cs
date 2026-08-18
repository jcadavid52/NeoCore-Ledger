using MassTransit;
using MediatR;
using NeoCore.Domain.EventosDominio.Transferencia;
using NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.TransferenciaEventos;

namespace NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.Publicadores
{
    public class NotificarTransferenciaCompletadaPublicador : INotificationHandler<TransferenciaCompletada>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public NotificarTransferenciaCompletadaPublicador(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(TransferenciaCompletada notification, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new NotificacionTransferenciaExitoso
            {
                TransferenciaId = notification.IdTransferencia
            },cancellationToken);
        }
    }
}
