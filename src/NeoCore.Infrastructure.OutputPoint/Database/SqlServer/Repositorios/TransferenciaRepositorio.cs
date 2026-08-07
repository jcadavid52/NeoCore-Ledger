using Microsoft.EntityFrameworkCore;
using NeoCore.Domain.Agregados;
using NeoCore.Domain.Excepciones;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Repositorios
{
    public class TransferenciaRepositorio : ITransferenciaRepositorio
    {
        private readonly SqlServerContexto _contexto;

        public TransferenciaRepositorio(SqlServerContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<TransferenciaAgregado?> ObtenerPorId(Guid id, CancellationToken cancellationToken)
        {
            var transferencia = await _contexto.TransferenciaAgregado
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return transferencia;
        }

        public async Task GuardarAsync(TransferenciaAgregado transferencia, CancellationToken cancellationToken)
        {
            await _contexto.TransferenciaAgregado.AddAsync(transferencia);
        }
    }
}
