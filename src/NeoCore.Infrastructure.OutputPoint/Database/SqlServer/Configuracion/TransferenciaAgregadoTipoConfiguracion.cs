using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeoCore.Domain.Agregados;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Configuracion
{
    public class TransferenciaAgregadoTipoConfiguracion : IEntityTypeConfiguration<TransferenciaAgregado>
    {
        public void Configure(EntityTypeBuilder<TransferenciaAgregado> builder)
        {
            builder.ToTable("TransferenciaAgregado", "ms-libro-mayor");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.IdCuentaOrigen).IsRequired();
            builder.Property(x => x.IdCuentaDestino).IsRequired();
            builder.Property(x => x.Monto).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.FechaCreacion).IsRequired();
            builder.Property(x => x.Estado).IsRequired();
            builder.Property(x => x.FechaFin).IsRequired(false);
            builder.Property(x => x.MotivoRechazo).IsRequired(false);
        }
    }
}
