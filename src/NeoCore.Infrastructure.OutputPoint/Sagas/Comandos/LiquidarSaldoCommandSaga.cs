namespace NeoCore.Infrastructure.OutputPoint.Sagas.Comandos
{
    public record LiquidarSaldoCommandSaga
    {
        public Guid IdCorrelacion { get; init; }
        public Guid IdCuentaOrigen { get; init; }
        public decimal Monto { get; init; }
    }
}
