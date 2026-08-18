using MediatR;

namespace NeoCore.Application.CasosDeUso.Transferencias.Comandos
{
    public record NotificarTransferenciaComando(Guid Idtransferencia) : IRequest;
}
