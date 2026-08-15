using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Application.CasosDeUso.LibroContable.Manejadores
{
    public class LiquidarSaldoManejador : IRequestHandler<LiquidarSaldoComando>
    {
        private readonly ILibroContableRepositorio _repositorio;
        private readonly IUnidadDeTrabajo _unitOfWork;

        public LiquidarSaldoManejador(
            ILibroContableRepositorio repositorio,
            IUnidadDeTrabajo unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(LiquidarSaldoComando comando, CancellationToken cancellationToken)
        {
            var cuenta = await _repositorio.CargarAsync(comando.IdCuentaOrigen, cancellationToken);
            cuenta.LiquidarBloqueo(comando.Monto, comando.IdCorrelacion);

            await _repositorio.GuardarAsync(cuenta, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
