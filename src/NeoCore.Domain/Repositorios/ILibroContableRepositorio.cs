using NeoCore.Domain.Agregados;

namespace NeoCore.Domain.Repositorios
{
    public interface ILibroContableRepositorio
    {
        Task<LibroContableAgregado> CargarAsync(Guid idCuenta, CancellationToken cancellationToken);
        Task GuardarAsync(LibroContableAgregado cuentaContable, CancellationToken cancellationToken);
    }
}
