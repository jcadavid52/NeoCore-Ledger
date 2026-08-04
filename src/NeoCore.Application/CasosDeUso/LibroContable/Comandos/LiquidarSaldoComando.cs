using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos.Resultados;
using NeoCore.SharedKernel.LibroContable;

namespace NeoCore.Application.CasosDeUso.LibroContable.Comandos
{
    public record LiquidarSaldoComando(
        Guid IdCuentaOrigen,
        decimal Monto,
        Guid IdCorrelacion,
        TipoCorrelacionEnum TipoCorrelacion) : IRequest<OperacionResultado>;
}
