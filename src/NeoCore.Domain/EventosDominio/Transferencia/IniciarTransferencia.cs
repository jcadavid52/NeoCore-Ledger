using NeoCore.Domain.Abstracciones;

namespace NeoCore.Domain.EventosDominio.Transferencia
{
    public record IniciarTransferencia : EventoDominio
    {
        public DateTime FechaInicio { get; init; }
        public Guid IdTransferencia { get; init; }
        public Guid IdCuentaOrigen { get; init; }
        public Guid IdCuentaDestino { get; init; }
        public decimal Monto { get; init; }

        public IniciarTransferencia(
            Guid idTransferencia,
            Guid idCuentaOrigen,
            Guid idCuentaDestino,
            decimal monto)
        {
            FechaInicio = DateTime.UtcNow;
            IdTransferencia = idTransferencia;
            IdCuentaOrigen = idCuentaOrigen;
            IdCuentaDestino = idCuentaDestino;
            Monto = monto;
        }
    }
}
