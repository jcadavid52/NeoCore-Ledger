using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Domain.Repositorios;
using NeoCore.SharedKernel.LibroContable;

namespace NeoCore.Application.CasosDeUso.LibroContable.Manejadores
{
    public class AcreditarSaldoManejador : IRequestHandler<AcreditarSaldoComando>
    {
        private readonly ILibroContableRepositorio _repositorio;
        private readonly IUnidadDeTrabajo _unitOfWork;

        public AcreditarSaldoManejador(
            ILibroContableRepositorio repositorio,
            IUnidadDeTrabajo unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AcreditarSaldoComando comando, CancellationToken cancellationToken)
        {
            var cuenta = await _repositorio.CargarAsync(comando.IdCuentaDestino, cancellationToken);
            cuenta.Acreditar(comando.Monto, comando.IdCorrelacion, TipoCorrelacionEnum.Transferencia);

            await _repositorio.GuardarAsync(cuenta, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
