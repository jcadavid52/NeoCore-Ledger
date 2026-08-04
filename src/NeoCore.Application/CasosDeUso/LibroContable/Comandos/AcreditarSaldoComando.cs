using MediatR;
using NeoCore.Application.CasosDeUso.LibroContable.Comandos.Resultados;
using NeoCore.SharedKernel.LibroContable;

namespace NeoCore.Application.CasosDeUso.LibroContable.Comandos
{
    public record AcreditarSaldoComando(
        Guid IdCuentaDestino,
        decimal Monto,
        Guid IdCorrelacion,
        TipoCorrelacionEnum TipoCorrelacion) : IRequest<OperacionResultado>;
}
