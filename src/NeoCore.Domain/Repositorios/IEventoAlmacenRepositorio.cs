using NeoCore.Domain.Interfaces;

namespace NeoCore.Domain.Repositorios
{
    public interface IEventoAlmacenRepositorio
    {
        Task<IReadOnlyCollection<IEventoDominio>> CargarAsync(
        Guid IdAgregado,
        CancellationToken cancellationToken);

        Task AgregarAsync(
            Guid idAgregado,
            IReadOnlyCollection<IEventoDominio> eventos,
            int versionEsperada,
            CancellationToken cancellationToken);
    }
}
