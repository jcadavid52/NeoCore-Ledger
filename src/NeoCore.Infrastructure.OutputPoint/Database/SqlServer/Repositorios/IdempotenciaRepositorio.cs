using Microsoft.EntityFrameworkCore;
using NeoCore.Domain.Repositorios;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Entidades;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Repositorios
{
    public class IdempotenciaRepositorio : IIdempotenciaRepositorio
    {
        private readonly SqlServerContexto _contexto;

        public IdempotenciaRepositorio(SqlServerContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> ExisteAsync(Guid idClave, CancellationToken cancellationToken)
        {
            return await _contexto.IdempotenciaEntidad
                .AnyAsync(e => e.IdClave == idClave, cancellationToken);
        }

        public async Task AgregarAsync(Guid idClave, string nombreComando, CancellationToken cancellationToken)
        {
            var entidad = new IdempotenciaEntidad
            {
                IdClave = idClave,
                NombreComando = nombreComando,
                CreadoEn = DateTime.UtcNow
            };

            _contexto.IdempotenciaEntidad.Add(entidad);
            await _contexto.SaveChangesAsync(cancellationToken);
        }
    }
}
