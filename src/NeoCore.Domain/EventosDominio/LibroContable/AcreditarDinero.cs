using NeoCore.Domain.Abstracciones;
using NeoCore.SharedKernel.LibroContable;

namespace NeoCore.Domain.EventosDominio.LibroContable
{
    public sealed record AcreditarDinero : EventoDominio
    {
        public decimal Monto { get; init; }
        public Guid IdCuenta { get; init; }
        public Guid IdCorrelacion { get; init; }
        public TipoCorrelacionEnum TipoCorrelacion { get; init; }

        public AcreditarDinero(
            decimal monto,
            Guid idCuenta,
            Guid idCorrelacion,
            TipoCorrelacionEnum tipoCorrelacion)
        {
            IdAgregado = idCuenta;
            IdCuenta = idCuenta;
            Monto = monto;
            IdCorrelacion = idCorrelacion;
            TipoCorrelacion = tipoCorrelacion;
        }
    }
}
