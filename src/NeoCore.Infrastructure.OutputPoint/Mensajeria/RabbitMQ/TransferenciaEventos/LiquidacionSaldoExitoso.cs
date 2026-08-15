namespace NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.TransferenciaEventos
{
    public record LiquidacionSaldoExitoso
    {
        public Guid TransferenciaId { get; init; }
    }
}
