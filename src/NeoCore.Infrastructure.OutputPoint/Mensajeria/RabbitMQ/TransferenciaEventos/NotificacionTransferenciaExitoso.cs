namespace NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.TransferenciaEventos
{
    public record NotificacionTransferenciaExitoso
    {
        public Guid TransferenciaId { get; init; }
    }
}
