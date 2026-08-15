namespace NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.TransferenciaEventos
{
    public record LiquidacionExitosa
    {
        public Guid TransferenciaId { get; init; }
    }
}
