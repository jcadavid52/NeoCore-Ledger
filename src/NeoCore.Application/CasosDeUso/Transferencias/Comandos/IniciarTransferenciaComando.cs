using MediatR;
using NeoCore.Application.CasosDeUso.Transferencias.Respuestas;
using NeoCore.Application.PuertosEntrada;

namespace NeoCore.Application.CasosDeUso.Transferencias.Comandos
{
    public record IniciarTransferenciaComando(
        Guid IdCuentaOrigen,
        Guid IdCuentaDestino,
        decimal Monto) : IRequest<IniciarTransferenciaRespuesta>, IComandoIdempotente;
}
