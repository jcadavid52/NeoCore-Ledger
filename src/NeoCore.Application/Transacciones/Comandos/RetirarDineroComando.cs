using MediatR;

namespace NeoCore.Application.Transacciones.Comandos
{
    public record RetirarDineroComando(Guid IdCuenta, decimal Monto) : IRequest;
}
