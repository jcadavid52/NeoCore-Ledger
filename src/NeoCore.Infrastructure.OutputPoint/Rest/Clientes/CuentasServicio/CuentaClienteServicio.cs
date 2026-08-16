using NeoCore.Application.PuertosSalida;
using NeoCore.Application.PuertosSalida.Respuestas;

namespace NeoCore.Infrastructure.OutputPoint.Rest.Clientes.CuentasServicio
{
    public class CuentaClienteServicio : ICuentaClienteServicio
    {
        private readonly HttpClient _httpClient;

        public CuentaClienteServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ValidarCuentaRespuesta> ObtenerInfoPorIdAsync(Guid idCuenta, CancellationToken cancellationToken)
        {

            //var response = await _httpClient.GetAsync($"/api/accounts/{idCuenta}/validate", cancellationToken);
            return new ValidarCuentaRespuesta(
                "Ahorros",
                "31181423962",
                "Activa",
                new UsuarioCuentaRespuesta(
                    "12345asd234",
                    "Camilo Cadavid",
                    "cadavidcamilo360@gmail.com",
                    "Vereda el mierdero"));
        }
    }
}
