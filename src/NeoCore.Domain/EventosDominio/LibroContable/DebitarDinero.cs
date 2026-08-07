using NeoCore.Domain.Abstracciones;

namespace NeoCore.Domain.EventosDominio.LibroContable
{
    public sealed record DebitarDinero : EventoDominio
    {
        public decimal Monto { get; init; }
        public Guid IdCuenta { get; init; }
        public Guid IdCorrelacion { get; init; }

        public DebitarDinero(decimal monto, Guid idCuenta, Guid idCorrelacion)
        {
            IdAgregado = idCuenta;
            IdCuenta = idCuenta;
            Monto = monto;
            IdCorrelacion = idCorrelacion;
        }
    }
}
