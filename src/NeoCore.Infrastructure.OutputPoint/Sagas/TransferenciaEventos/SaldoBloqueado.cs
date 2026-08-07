namespace NeoCore.Infrastructure.OutputPoint.Sagas.TransferenciaEventos
{
    public record SaldoBloqueado
    {
        public Guid TransferenciaId { get; init; }
    }
}
