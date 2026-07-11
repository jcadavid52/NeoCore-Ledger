namespace NeoCore.Domain.Repositorios
{
    public interface IIdempotenciaRepositorio
    {
        Task<bool> ExisteAsync(Guid idClave, CancellationToken cancellationToken);
        Task AgregarAsync(Guid idClave, string nombreComando, CancellationToken cancellationToken);
    }
}
