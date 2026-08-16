using MediatR;
using NeoCore.Application.CasosDeUso.Transferencias.Comandos;
using NeoCore.Domain.Excepciones;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Application.CasosDeUso.Transferencias.Manejadores
{
    public class NotificarTransferenciaManejador : IRequestHandler<NotificarTransferenciaComando>
    {
        private readonly ITransferenciaRepositorio _transferenciaRepositorio;
        private readonly IUnidadDeTrabajo _unitOfWork;

        public NotificarTransferenciaManejador(ITransferenciaRepositorio transferenciaRepositorio, IUnidadDeTrabajo unitOfWork)
        {
            _transferenciaRepositorio = transferenciaRepositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(NotificarTransferenciaComando request, CancellationToken cancellationToken)
        {
            var transferencia = await _transferenciaRepositorio.ObtenerPorId(
                request.Idtransferencia,
                cancellationToken);

            if (transferencia == null)
                throw new ExcepcionNoEncontrado($"No se encontró transferencia con id '{request.Idtransferencia}'");

            //TODO: proceso de notificar

            transferencia.Completada();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
