using MassTransit;
using MediatR;
using NeoCore.Domain.EventosDominio.Transferencia;
using NeoCore.SharedKernel.Transferencias;

namespace NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.Publicadores
{
    public class TransferenciaIniciadaPublicador : INotificationHandler<IniciarTransferencia>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public TransferenciaIniciadaPublicador(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(IniciarTransferencia notification, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new TransferenciaIniciada(
               notification.IdCuentaOrigen,
               notification.IdCuentaDestino,
               notification.Monto,
               notification.IdTransferencia), cancellationToken);
        }
    }
}
