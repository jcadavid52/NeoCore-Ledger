using NeoCore.Application.Interfaces;

namespace NeoCore.Infrastructure.OutputPoint.Rest.Clientes.CuentasServicio
{
    public class ServicioCuentaCliente : ICuentaServicioCliente
    {
        private readonly HttpClient _httpClient;

        public ServicioCuentaCliente(HttpClient httpClient)
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
