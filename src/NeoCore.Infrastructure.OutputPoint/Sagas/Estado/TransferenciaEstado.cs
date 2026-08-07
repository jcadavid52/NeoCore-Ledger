using MassTransit;

namespace NeoCore.Infrastructure.OutputPoint.Sagas.Estado
{
    public class TransferenciaEstado : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; } // = TransferenciaId
        public string EstadoActual { get; set; }

        // Datos de negocio que necesitas recordar durante todo el proceso
        public Guid CuentaOrigenId { get; set; }
        public Guid CuentaDestinoId { get; set; }
        public decimal Monto { get; set; }

        // Flags para saber qué pasos ya se ejecutaron (clave para compensar bien)
        public bool SaldoBloqueado { get; set; }
        public bool DestinoAcreditado { get; set; }
        public bool OrigenLiquidado { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string MotivoRechazo { get; set; }

        public byte[] RowVersion { get; set; } // para concurrencia optimista en BD
    }
}
