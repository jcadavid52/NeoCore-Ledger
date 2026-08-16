using MediatR;
using NeoCore.Application.CasosDeUso.Transferencias.Comandos;
using NeoCore.Application.CasosDeUso.Transferencias.Respuestas;
using NeoCore.Application.PuertosSalida;
using NeoCore.Domain.Agregados;
using NeoCore.Domain.Excepciones;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Application.CasosDeUso.Transferencias.Manejadores
{
    public class IniciarTransferenciaManejador : IRequestHandler<IniciarTransferenciaComando, IniciarTransferenciaRespuesta>
    {
        private readonly ICuentaClienteServicio _accountClient;
        private readonly ITransferenciaRepositorio _transferenciaRepositorio;
        private readonly IUnidadDeTrabajo _unitOfWork;

        public IniciarTransferenciaManejador(
            ICuentaClienteServicio accountClient,
            ITransferenciaRepositorio transferenciaRepositorio,
            IUnidadDeTrabajo unitOfWork)
        {
            _accountClient = accountClient;
            _transferenciaRepositorio = transferenciaRepositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task<IniciarTransferenciaRespuesta> Handle(IniciarTransferenciaComando comando, CancellationToken cancellationToken)
        {
            var cuentaUsuario = await _accountClient.ObtenerInfoPorIdAsync(comando.IdCuentaDestino, cancellationToken);
            if (cuentaUsuario.Estado == "Invalida")
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
