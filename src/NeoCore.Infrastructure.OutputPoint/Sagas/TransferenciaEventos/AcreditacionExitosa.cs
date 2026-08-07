namespace NeoCore.Infrastructure.OutputPoint.Sagas.TransferenciaEventos
{
    public record AcreditacionExitosa
    {
        public Guid TransferenciaId { get; init; }
    }
}
