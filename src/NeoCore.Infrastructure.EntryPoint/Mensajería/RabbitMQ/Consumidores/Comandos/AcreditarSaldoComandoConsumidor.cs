namespace NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Consumidores.Comandos
{
    public class AcreditarSaldoComandoConsumidor
    {
        public Guid IdCorrelacion { get; init; }
        public Guid IdCuentaDestino { get; init; }
        public decimal Monto { get; init; }
    }
}
