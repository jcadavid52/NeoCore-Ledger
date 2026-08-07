using MediatR;
using NeoCore.Application.CasosDeUso.Transferencias.Comandos;
using NeoCore.Application.CasosDeUso.Transferencias.Respuestas;
using NeoCore.Application.Interfaces;
using NeoCore.Domain.Agregados;
using NeoCore.Domain.Excepciones;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Application.CasosDeUso.Transferencias.Manejadores
{
    public class IniciarTransferenciaManejador: IRequestHandler<IniciarTransferenciaComando, IniciarTransferenciaRespuesta>
    {
        private readonly ICuentaServicioCliente _accountClient;
        private readonly ITransferenciaRepositorio _transferenciaRepositorio;
        private readonly IUnidadDeTrabajo _unitOfWork;

        public IniciarTransferenciaManejador(
            ICuentaServicioCliente accountClient,
            ITransferenciaRepositorio transferenciaRepositorio,
            IUnidadDeTrabajo unitOfWork)
        {
            _accountClient = accountClient;
            _transferenciaRepositorio = transferenciaRepositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task<IniciarTransferenciaRespuesta> Handle(IniciarTransferenciaComando comando, CancellationToken cancellationToken)
        {
            var destinoValido = await _accountClient.ValidarCuentaAsync(comando.IdCuentaDestino, cancellationToken);
            if (!destinoValido)
                throw new ExcepcionConflicto($"La cuenta destino {comando.IdCuentaDestino} no es válida");
           
            var transferencia = new TransferenciaAgregado();

            transferencia.Iniciar(
                comando.Monto,
                comando.IdCuentaOrigen,
                comando.IdCuentaDestino);

            await _transferenciaRepositorio.GuardarAsync(transferencia, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new IniciarTransferenciaRespuesta(transferencia.Id);
        }
    }
}
