namespace NeoCore.Domain.Interfaces
{
    public interface ITieneEventosDominio
    {
        IReadOnlyCollection<IEventoDominio> ObtenerEventosNoConfirmados { get; }
        void LimpiarEventosNoConfirmados();
        void AgregarEvento(IEventoDominio evento);
    }
}
