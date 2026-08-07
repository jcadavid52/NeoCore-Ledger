using MediatR;
using NeoCore.SharedKernel.LibroContable;

namespace NeoCore.Application.CasosDeUso.LibroContable.Comandos
{
    public record AcreditarSaldoComando(
        Guid IdCuentaDestino,
        decimal Monto,
        Guid IdCorrelacion,
        TipoCorrelacionEnum TipoCorrelacion) : IRequest;
}
