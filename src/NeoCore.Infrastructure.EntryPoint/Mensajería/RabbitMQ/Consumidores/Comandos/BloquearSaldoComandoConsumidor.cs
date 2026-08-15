namespace NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Consumidores.Comandos
{
    public record BloquearSaldoComandoConsumidor
    {
        public Guid IdCorrelacion { get; init; }
        public Guid IdCuentaOrigen { get; init; }
        public decimal Monto { get; init; }
    }
}
