using NeoCore.Domain.Abstracciones;
using NeoCore.Domain.Enums;
using NeoCore.Domain.EventosDominio.Transferencia;
using NeoCore.Domain.Excepciones;

namespace NeoCore.Domain.Agregados
{
    public class TransferenciaAgregado : AgregadoBase
    {
        public Guid Id { get; private set; }
        public Guid IdCuentaOrigen { get; private set; }
        public Guid IdCuentaDestino { get; private set; }
        public decimal Monto { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public EstadoTransferencia Estado { get; private set; }
        public DateTime? FechaFin { get; private set; }
        public string? MotivoRechazo { get; private set; }

        public void Iniciar(
            decimal monto,
            Guid idCuentaOrigen,
            Guid idCuentaDestino)
        {
            if (monto <= 0)
                throw new DominioExcepcion("Monto inválido");

            Id = Guid.NewGuid();
            IdCuentaOrigen = idCuentaOrigen;
            IdCuentaDestino = idCuentaDestino;
            FechaCreacion = DateTime.UtcNow;
            Monto = monto;
            Estado = EstadoTransferencia.Pendiente;

            AgregarEvento(new IniciarTransferencia(Id, IdCuentaOrigen, IdCuentaDestino, Monto));
        }
    }
}
