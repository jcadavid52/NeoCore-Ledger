namespace NeoCore.Application.PuertosSalida
{
    public interface ICuentaServicioCliente
    {
        Task<bool> ValidarCuentaAsync(Guid idCuenta, CancellationToken cancellationToken);
    }
}
