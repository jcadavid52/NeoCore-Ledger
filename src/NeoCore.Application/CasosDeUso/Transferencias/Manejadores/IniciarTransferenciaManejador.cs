using MassTransit;
using MediatR;
using NeoCore.Application.CasosDeUso.Transferencias.Comandos;
using NeoCore.Application.CasosDeUso.Transferencias.Respuestas;
using NeoCore.Application.Interfaces;
using NeoCore.Domain.Agregados;
using NeoCore.Domain.Excepciones;
using NeoCore.Domain.Repositorios;
using NeoCore.SharedKernel.Transferencias;

namespace NeoCore.Application.CasosDeUso.Transferencias.Manejadores
{
    public class IniciarTransferenciaManejador: IRequestHandler<IniciarTransferenciaComando, IniciarTransferenciaRespuesta>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ICuentaServicioCliente _accountClient;
        private readonly IUnidadDeTrabajo _unitOfWork;

        public IniciarTransferenciaManejador(
            IPublishEndpoint publishEndpoint,
            ICuentaServicioCliente accountClient,
            IUnidadDeTrabajo unitOfWork)
        {
            _publishEndpoint = publishEndpoint;
            _accountClient = accountClient;
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

            await _publishEndpoint.Publish(new TransferenciaIniciada(
                transferencia.IdCuentaOrigen,
                transferencia.IdCuentaDestino,
                transferencia.Monto,
                transferencia.Id), cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new IniciarTransferenciaRespuesta(transferencia.Id);
        }
    }
}
