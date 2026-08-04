using MassTransit;
using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos.Resultados;
using NeoCore.Domain.Repositorios;
using NeoCore.SharedKernel.LibroContable;

namespace NeoCore.Application.CasosDeUso.LibroContable.Manejadores
{
    public class AcreditarSaldoManejador : IRequestHandler<AcreditarSaldoComando, OperacionResultado>
    {
        private readonly ILibroContableRepositorio _repositorio;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUnidadDeTrabajo _unitOfWork;

        public AcreditarSaldoManejador(
            ILibroContableRepositorio repositorio,
            IPublishEndpoint publishEndpoint,
            IUnidadDeTrabajo unitOfWork)
        {
            _repositorio = repositorio;
            _publishEndpoint = publishEndpoint;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperacionResultado> Handle(AcreditarSaldoComando comando, CancellationToken cancellationToken)
        {
            var cuenta = await _repositorio.CargarAsync(comando.IdCuentaDestino, cancellationToken);
            cuenta.Acreditar(comando.Monto,comando.IdCorrelacion, TipoCorrelacionEnum.Transferencia);

            await _repositorio.GuardarAsync(cuenta, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new OperacionResultado(true, string.Empty);
        }
    }
}
