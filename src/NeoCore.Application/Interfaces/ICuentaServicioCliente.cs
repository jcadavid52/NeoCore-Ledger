namespace NeoCore.Application.Interfaces
{
    public interface ICuentaServicioCliente
    {
        Task<bool> ValidarCuentaAsync(Guid idCuenta, CancellationToken cancellationToken);
    }
}
