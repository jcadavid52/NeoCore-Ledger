using NeoCore.Domain.Abstracciones;

namespace NeoCore.Domain.Eventos
{
    public sealed record RetirarDinero: EventoDominio
    {
        public decimal Monto { get; init; }
        public Guid IdCuenta { get; init; }

        public RetirarDinero(decimal monto,Guid idCuenta)
        {
            IdAgregado = idCuenta;
            IdCuenta = idCuenta;
            Monto = monto;
        }
    }
}
