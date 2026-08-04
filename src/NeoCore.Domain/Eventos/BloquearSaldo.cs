using NeoCore.Domain.Abstracciones;
using NeoCore.SharedKernel.LibroContable;

namespace NeoCore.Domain.Eventos
{
    public sealed record BloquearSaldo : EventoDominio
    {
        public decimal Monto { get; init; }
        public Guid IdCuenta { get; init; }
        public Guid IdCorrelacion { get; init; }
        public TipoCorrelacionEnum TipoCorrelacion { get; init; }

        public BloquearSaldo(
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
