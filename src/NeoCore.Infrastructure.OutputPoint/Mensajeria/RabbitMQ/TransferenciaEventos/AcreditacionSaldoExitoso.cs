namespace NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.TransferenciaEventos
{
    public record AcreditacionSaldoExitoso
    {
        public Guid TransferenciaId { get; init; }
    }
}
