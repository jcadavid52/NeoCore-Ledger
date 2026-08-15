namespace NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.TransferenciaEventos
{
    public record AcreditacionExitosa
    {
        public Guid TransferenciaId { get; init; }
    }
}
