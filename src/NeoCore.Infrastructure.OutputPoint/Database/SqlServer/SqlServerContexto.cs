using Microsoft.EntityFrameworkCore;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Entidades;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer
{
    public class SqlServerContexto:DbContext
    {
        private const string NombreEsquema = "ms-libro-mayor";

        public DbSet<EventoAlmacenEntidad> EventoAlmacenEntidad { get; set; }

        public SqlServerContexto(DbContextOptions<SqlServerContexto> opciones):base(opciones)
        {
            EventoAlmacenEntidad = Set<EventoAlmacenEntidad>();
        }

        protected override void OnModelCreating(ModelBuilder modeloConstructor)
        {
            base.OnModelCreating(modeloConstructor);

            modeloConstructor.HasDefaultSchema(NombreEsquema);

            modeloConstructor.ApplyConfiguration(new EventoAlmacenEntidadConfiguracion());
        }
    }
}
