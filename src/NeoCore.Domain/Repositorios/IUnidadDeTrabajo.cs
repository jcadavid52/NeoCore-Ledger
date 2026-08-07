using NeoCore.Domain.Interfaces;

namespace NeoCore.Domain.Repositorios
{
    public interface IUnidadDeTrabajo
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void RegistrarAgregado(ITieneEventosDominio agregado);
    }
}
