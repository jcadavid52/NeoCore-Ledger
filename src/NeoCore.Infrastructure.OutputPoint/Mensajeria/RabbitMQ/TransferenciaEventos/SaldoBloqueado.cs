namespace NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.TransferenciaEventos
{
    public record SaldoBloqueado
    {
        public Guid TransferenciaId { get; init; }
    }
}
