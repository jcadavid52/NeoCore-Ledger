namespace NeoCore.Infrastructure.OutputPoint.Sagas.TransferenciaEventos
{
    public record NotificacionCompleta
    {
        public Guid TransferenciaId { get; init; }
    }
}
