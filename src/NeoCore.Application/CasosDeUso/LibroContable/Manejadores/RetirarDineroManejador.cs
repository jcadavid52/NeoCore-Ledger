using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Application.CasosDeUso.LibroContable.Manejadores
{
    public class RetirarDineroManejador:IRequestHandler<RetirarDineroComando>
    {
        private readonly ILibroContableRepositorio _repositorio;

        public RetirarDineroManejador(ILibroContableRepositorio repositorio)
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
