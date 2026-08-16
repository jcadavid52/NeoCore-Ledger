using NeoCore.Application.PuertosSalida.Respuestas;

namespace NeoCore.Application.PuertosSalida
{
    public interface ICuentaClienteServicio
    {
        Task<ValidarCuentaRespuesta> ObtenerInfoPorIdAsync(
            Guid idCuenta,
            CancellationToken cancellationToken);
    }
}
