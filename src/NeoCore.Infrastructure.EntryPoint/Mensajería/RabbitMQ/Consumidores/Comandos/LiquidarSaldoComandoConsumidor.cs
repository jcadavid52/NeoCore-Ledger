namespace NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Consumidores.Comandos
{
    public record LiquidarSaldoComandoConsumidor
    {
        public Guid IdCorrelacion { get; init; }
        public Guid IdCuentaOrigen { get; init; }
        public decimal Monto { get; init; }
    }
}
