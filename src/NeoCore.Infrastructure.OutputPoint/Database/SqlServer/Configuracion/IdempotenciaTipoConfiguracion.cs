using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Entidades;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Configuracion
{
    public class IdempotenciaTipoConfiguracion : IEntityTypeConfiguration<IdempotenciaEntidad>
    {
        public void Configure(EntityTypeBuilder<IdempotenciaEntidad> entity)
        {
            entity.ToTable("Idempotencias", "ms-libro-mayor");

            entity.HasKey(e => e.IdClave);

            entity.Property(e => e.NombreComando)
                  .IsRequired()
                  .HasMaxLength(256);

            entity.Property(e => e.CreadoEn)
                  .IsRequired();
        }
    }
}
