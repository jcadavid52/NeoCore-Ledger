using NeoCore.Domain.Abstracciones;

namespace NeoCore.Domain.Eventos
{
    public sealed record DepositarDinero : EventoDominio
    {
        public decimal Monto { get; init; }
        public Guid IdCuentaDestino { get; init; }
        public Guid IdCuentaOrigen { get; init; }

        public DepositarDinero(decimal monto, Guid idCuentaDestino,Guid idCuentaOrigen)
        {
            IdAgregado = idCuentaDestino;
            IdCuentaDestino = idCuentaDestino;
            Monto = monto;
            IdCuentaOrigen = idCuentaOrigen;
        }
    }
}
