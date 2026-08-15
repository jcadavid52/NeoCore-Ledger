namespace NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.TransferenciaEventos
{
    public record SaldoBloqueadoExitoso
    {
        public Guid TransferenciaId { get; init; }
    }
}
