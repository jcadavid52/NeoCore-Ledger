using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Entidades;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Configuracion
{
    public class EventoAlmacenTipoConfiguracion : IEntityTypeConfiguration<EventoAlmacenEntidad>
    {
        public void Configure(EntityTypeBuilder<EventoAlmacenEntidad> entity)
        {
            entity.ToTable("Eventos", "ms-libro-mayor");

            entity.HasKey(e => e.IdEvento);

            entity.Property(e => e.IdAgregado)
                  .IsRequired();

            entity.Property(e => e.Version)
                  .IsRequired();

            entity.Property(e => e.TipoMensaje)
                  .IsRequired()
                  .HasMaxLength(256);

            entity.Property(e => e.Dato)
                  .IsRequired()
                  .HasColumnType("nvarchar(max)");

            entity.Property(e => e.OcurrioEn)
                  .IsRequired();

            entity.HasIndex(e => new { e.IdAgregado, e.Version })
                  .IsUnique()
                  .HasDatabaseName("IX_Eventos_Agregado_Version");

            entity.HasIndex(e => e.TipoMensaje)
                  .HasDatabaseName("IX_Eventos_TipoMensaje");

            entity.HasIndex(e => e.OcurrioEn)
                  .HasDatabaseName("IX_Eventos_OcurrioEn");
        }
    }
}
