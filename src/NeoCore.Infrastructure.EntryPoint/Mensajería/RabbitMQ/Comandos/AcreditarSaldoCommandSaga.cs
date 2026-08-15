namespace NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Comandos
{
    public class AcreditarSaldoCommandSaga
    {
        public Guid IdCorrelacion { get; init; }
        public Guid IdCuentaDestino { get; init; }
        public decimal Monto { get; init; }
    }
}
