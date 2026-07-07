using NeoCore.Application.CuentasContables.Comandos;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Application.CuentasContables.Manejadores
{
    public class RetirarDineroManejador
    {
        private readonly ICuentaContableRepositorio _repositorio;

        public RetirarDineroManejador(ICuentaContableRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task Ejecutar(RetirarDineroComando comando,CancellationToken cancellationToken)
        {
            var cuenta = await _repositorio.CargarAsync(comando.IdCuenta, cancellationToken);

            cuenta.Retirar(comando.Monto);

            await _repositorio.GuardarAsync(cuenta,cancellationToken);
        }
    }
}
