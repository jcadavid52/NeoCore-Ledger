namespace NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Consumidores.Comandos
{
    public record NotificarTransferenciaComandoConsumidor
    {
        public Guid IdCorrelacion { get; init; }
    }
}
