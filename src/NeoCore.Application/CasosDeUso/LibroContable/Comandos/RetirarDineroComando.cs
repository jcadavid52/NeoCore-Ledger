using MediatR;
using NeoCore.Application.PuertosEntrada;

namespace NeoCore.Application.CasosDeUso.LibroContable.Comandos
{
    public record RetirarDineroComando(
        Guid IdCuenta,
        decimal Monto) : IRequest, IComandoIdempotente;
}
