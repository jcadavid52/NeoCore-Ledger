using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Entidades
{
    public class IdempotenciaEntidadConfiguracion : IEntityTypeConfiguration<IdempotenciaEntidad>
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
