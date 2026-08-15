namespace NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Comandos
{
    public record LiquidarSaldoCommandSaga
    {
        public Guid IdCorrelacion { get; init; }
        public Guid IdCuentaOrigen { get; init; }
        public decimal Monto { get; init; }
    }
}
