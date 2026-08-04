using MassTransit;
using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos.Resultados;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Application.CasosDeUso.LibroContable.Manejadores
{
    public class LiquidarSaldoManejador : IRequestHandler<LiquidarSaldoComando, OperacionResultado>
    {
        private readonly ILibroContableRepositorio _repositorio;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUnidadDeTrabajo _unitOfWork;

        public LiquidarSaldoManejador(
            ILibroContableRepositorio repositorio,
            IPublishEndpoint publishEndpoint,
            IUnidadDeTrabajo unitOfWork)
        {
            _repositorio = repositorio;
            _publishEndpoint = publishEndpoint;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperacionResultado> Handle(LiquidarSaldoComando comando, CancellationToken cancellationToken)
        {
            var cuenta = await _repositorio.CargarAsync(comando.IdCuentaOrigen, cancellationToken);
            cuenta.LiquidarBloqueo(comando.Monto);

            await _repositorio.GuardarAsync(cuenta, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new OperacionResultado(true, string.Empty);
        }
    }
}
