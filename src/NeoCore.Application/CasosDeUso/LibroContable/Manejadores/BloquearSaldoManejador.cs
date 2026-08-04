using MassTransit;
using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos.Resultados;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Application.CasosDeUso.LibroContable.Manejadores
{
    public class BloquearSaldoManejador : IRequestHandler<BloquearSaldoComando, OperacionResultado>
    {
        private readonly ILibroContableRepositorio _repositorio;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUnidadDeTrabajo _unitOfWork;

        public BloquearSaldoManejador(
            ILibroContableRepositorio repositorio,
            IPublishEndpoint publishEndpoint,
            IUnidadDeTrabajo unitOfWork)
        {
            _repositorio = repositorio;
            _publishEndpoint = publishEndpoint;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperacionResultado> Handle(BloquearSaldoComando comando, CancellationToken cancellationToken)
        {
            var cuenta = await _repositorio.CargarAsync(comando.IdCuenta, cancellationToken);

            cuenta.BloquearSaldo(
                comando.Monto,
                comando.IdCorrelacion,
                comando.TipoCorrelacion);

            await _repositorio.GuardarAsync(cuenta, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new OperacionResultado(true, string.Empty);
        }
    }
}
