namespace NeoCore.Infrastructure.OutputPoint.Sagas.Comandos
{
    public class AcreditarSaldoCommandSaga
    {
        public Guid IdCorrelacion { get; init; }
        public Guid IdCuentaDestino { get; init; }
        public decimal Monto { get; init; }
    }
}
