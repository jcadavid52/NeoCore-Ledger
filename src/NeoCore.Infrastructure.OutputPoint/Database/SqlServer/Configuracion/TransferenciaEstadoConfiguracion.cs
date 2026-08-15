using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.Estado;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Configuracion
{
    public class TransferenciaEstadoConfiguracion : IEntityTypeConfiguration<TransferenciaEstado>
    {
        public void Configure(EntityTypeBuilder<TransferenciaEstado> builder)
        {
            builder.ToTable("TransferenciaEstado", "ms-libro-mayor");

            builder.HasKey(x => x.CorrelationId);
            builder.Property(x => x.CorrelationId).ValueGeneratedNever();
            builder.Property(x => x.EstadoActual).HasMaxLength(64).IsRequired();
            builder.Property(x => x.CuentaOrigenId).IsRequired();
            builder.Property(x => x.CuentaDestinoId).IsRequired();
            builder.Property(x => x.Monto).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.SaldoBloqueado).IsRequired();
            builder.Property(x => x.DestinoAcreditado).IsRequired();
            builder.Property(x => x.OrigenLiquidado).IsRequired();
            builder.Property(x => x.FechaInicio).IsRequired();
            builder.Property(x => x.FechaFin).IsRequired(false);
            builder.Property(x => x.MotivoRechazo).IsRequired(false);
            builder.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
