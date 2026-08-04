using Microsoft.EntityFrameworkCore;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Configuracion;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Entidades;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer
{
    public class SqlServerContexto : DbContext
    {
        private const string NombreEsquema = "ms-libro-mayor";

        public DbSet<EventoAlmacenEntidad> EventoAlmacenEntidad { get; set; }
        public DbSet<IdempotenciaEntidad> IdempotenciaEntidad { get; set; }

        public SqlServerContexto(DbContextOptions<SqlServerContexto> opciones) : base(opciones)
        {
            EventoAlmacenEntidad = Set<EventoAlmacenEntidad>();
            IdempotenciaEntidad = Set<IdempotenciaEntidad>();
        }

        protected override void OnModelCreating(ModelBuilder modeloConstructor)
        {
            base.OnModelCreating(modeloConstructor);

            modeloConstructor.HasDefaultSchema(NombreEsquema);

            modeloConstructor.ApplyConfiguration(new EventoAlmacenTipoConfiguracion());
            modeloConstructor.ApplyConfiguration(new IdempotenciaTipoConfiguracion());
        }
    }
}
