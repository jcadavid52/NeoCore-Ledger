using MediatR;
using NeoCore.SharedKernel.LibroContable;

namespace NeoCore.Application.CasosDeUso.LibroContable.Comandos
{
    public record BloquearSaldoComando(
        Guid IdCuenta,
        decimal Monto,
        Guid IdCorrelacion,
        TipoCorrelacionEnum TipoCorrelacion) : IRequest;
}
