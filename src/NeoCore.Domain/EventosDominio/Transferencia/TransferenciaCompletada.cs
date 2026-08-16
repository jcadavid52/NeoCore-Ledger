using NeoCore.Domain.Abstracciones;

namespace NeoCore.Domain.EventosDominio.Transferencia
{
    public record TransferenciaCompletada : EventoDominio
    {
        public Guid IdTransferencia { get; init; }

        public TransferenciaCompletada(Guid idTransferencia)
        {
            IdTransferencia = idTransferencia;
        }
    }
}
