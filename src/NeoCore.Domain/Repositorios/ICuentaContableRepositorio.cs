using NeoCore.Domain.Agregados;

namespace NeoCore.Domain.Repositorios
{
    public interface ICuentaContableRepositorio
    {
        Task<CuentaContable> CargarAsync(Guid idCuenta, CancellationToken cancellationToken);
        Task GuardarAsync(CuentaContable cuentaContable, CancellationToken cancellationToken);
    }
}
