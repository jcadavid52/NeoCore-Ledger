using NeoCore.Application.PuertosSalida;

namespace NeoCore.Infrastructure.OutputPoint.Rest.Clientes.CuentasServicio
{
    public class CuentaClienteServicio : ICuentaServicioCliente
    {
        private readonly HttpClient _httpClient;

        public CuentaClienteServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ValidarCuentaAsync(Guid idCuenta, CancellationToken cancellationToken)
        {

            //var response = await _httpClient.GetAsync($"/api/accounts/{idCuenta}/validate", cancellationToken);
            return true;

        }
    }
}
