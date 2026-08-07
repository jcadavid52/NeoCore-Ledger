namespace NeoCore.Infrastructure.OutputPoint.Sagas.TransferenciaEventos
{
    public record LiquidacionExitosa
    {
        public Guid TransferenciaId { get; init; }
    }
}
