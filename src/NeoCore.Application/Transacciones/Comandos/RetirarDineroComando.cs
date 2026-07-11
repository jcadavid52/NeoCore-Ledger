using MediatR;
using NeoCore.Application.Interfaces;

namespace NeoCore.Application.Transacciones.Comandos
{
    public record RetirarDineroComando(Guid IdCuenta, decimal Monto) : IRequest, IComandoIdempotente;
}
