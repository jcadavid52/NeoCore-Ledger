using NeoCore.Domain.Agregados;

namespace NeoCore.Domain.Repositorios
{
    public interface ITransferenciaRepositorio
    {
        Task<TransferenciaAgregado?> ObtenerPorId(Guid id, CancellationToken cancellationToken);
        Task GuardarAsync(TransferenciaAgregado transferencia, CancellationToken cancellationToken);
    }
}
