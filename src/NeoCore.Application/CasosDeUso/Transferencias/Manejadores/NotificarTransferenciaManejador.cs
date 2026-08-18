using MediatR;
using NeoCore.Application.CasosDeUso.Transferencias.Comandos;
using NeoCore.Application.PuertosSalida;
using NeoCore.Domain.Excepciones;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Application.CasosDeUso.Transferencias.Manejadores
{
    public class NotificarTransferenciaManejador : IRequestHandler<NotificarTransferenciaComando>
    {
        private readonly ITransferenciaRepositorio _transferenciaRepositorio;
        private readonly IUnidadDeTrabajo _unitOfWork;
        private readonly ICuentaClienteServicio _accountClient;

        public NotificarTransferenciaManejador(
            ITransferenciaRepositorio transferenciaRepositorio,
            IUnidadDeTrabajo unitOfWork,
            ICuentaClienteServicio accountClient)
        {
            _transferenciaRepositorio = transferenciaRepositorio;
            _unitOfWork = unitOfWork;
            _accountClient = accountClient;
        }

        public async Task Handle(NotificarTransferenciaComando request, CancellationToken cancellationToken)
        {
            var transferencia = await _transferenciaRepositorio.ObtenerPorId(
                request.Idtransferencia,
                cancellationToken);

            if (transferencia == null)
                throw new ExcepcionNoEncontrado($"No se encontró transferencia con id '{request.Idtransferencia}'");

            var cuentaUsuarioOrigen =
                await _accountClient.ObtenerInfoPorIdAsync(transferencia.IdCuentaOrigen, cancellationToken);

            var cuentaUsuarioDestino =
                await _accountClient.ObtenerInfoPorIdAsync(transferencia.IdCuentaDestino, cancellationToken);


            //TODO: proceso de notificar

            transferencia.Completada();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
