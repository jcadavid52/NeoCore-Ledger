using NeoCore.Domain.Repositorios;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer
{
    public class UnidadDeTrabajoSqlServer : IUnidadDeTrabajo
    {
        private readonly SqlServerContexto _contexto;

        public UnidadDeTrabajoSqlServer(SqlServerContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _contexto.SaveChangesAsync(cancellationToken);
        }
    }
}
