using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Application.CasosDeUso.LibroContable.Manejadores
{
    public class BloquearSaldoManejador : IRequestHandler<BloquearSaldoComando>
    {
        private readonly ILibroContableRepositorio _repositorio;
        private readonly IUnidadDeTrabajo _unitOfWork;

        public BloquearSaldoManejador(
            ILibroContableRepositorio repositorio,
            IUnidadDeTrabajo unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(BloquearSaldoComando comando, CancellationToken cancellationToken)
        {
            var cuenta = await _repositorio.CargarAsync(comando.IdCuenta, cancellationToken);

            cuenta.BloquearSaldo(
                comando.Monto,
                comando.IdCorrelacion,
                comando.TipoCorrelacion);

            await _repositorio.GuardarAsync(cuenta, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
