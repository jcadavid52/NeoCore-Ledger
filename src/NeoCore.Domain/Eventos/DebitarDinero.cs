using NeoCore.Domain.Abstracciones;

namespace NeoCore.Domain.Eventos
{
    public sealed record DebitarDinero : EventoDominio
    {
        public decimal Monto { get; init; }
        public Guid IdCuenta { get; init; }

        public DebitarDinero(decimal monto, Guid idCuenta)
        {
            IdAgregado = idCuenta;
            IdCuenta = idCuenta;
            Monto = monto;
        }
    }
}
