namespace NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.TransferenciaEventos
{
    public record NotificacionCompleta
    {
        public Guid TransferenciaId { get; init; }
    }
}
