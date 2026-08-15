using NeoCore.Domain.Interfaces;

namespace NeoCore.Domain.Abstracciones
{
    public abstract class AgregadoBase : ITieneEventosDominio
    {
        private readonly List<IEventoDominio> _eventosDominio = new();

        public IReadOnlyCollection<IEventoDominio> ObtenerEventosNoConfirmados
            => _eventosDominio.AsReadOnly();

        public void AgregarEvento(IEventoDominio evento)
            => _eventosDominio.Add(evento);

        public void LimpiarEventosNoConfirmados()
            => _eventosDominio.Clear();
    }
}
