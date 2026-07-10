using MediatR;
using NeoCore.Application.Transacciones.Comandos;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Application.Transacciones.Manejadores
{
    public class RetirarDineroManejador:IRequestHandler<RetirarDineroComando>
    {
        private readonly ICuentaContableRepositorio _repositorio;

        public RetirarDineroManejador(ICuentaContableRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task Handle(RetirarDineroComando comando, CancellationToken cancellationToken)
        {
            var cuenta = await _repositorio.CargarAsync(comando.IdCuenta, cancellationToken);

            cuenta.Retirar(comando.Monto);

            await _repositorio.GuardarAsync(cuenta, cancellationToken);
        }
    }
}
