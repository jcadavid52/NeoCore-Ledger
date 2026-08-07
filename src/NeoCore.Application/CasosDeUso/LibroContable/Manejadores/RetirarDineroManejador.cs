using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Application.CasosDeUso.LibroContable.Manejadores
{
    public class RetirarDineroManejador:IRequestHandler<RetirarDineroComando>
    {
        private readonly ILibroContableRepositorio _repositorio;
        private readonly IUnidadDeTrabajo _unitOfWork;

        public RetirarDineroManejador(ILibroContableRepositorio repositorio, IUnidadDeTrabajo unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RetirarDineroComando comando, CancellationToken cancellationToken)
        {
            var cuenta = await _repositorio.CargarAsync(comando.IdCuenta, cancellationToken);

            cuenta.Retirar(comando.Monto,Guid.NewGuid());

            await _repositorio.GuardarAsync(cuenta, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
