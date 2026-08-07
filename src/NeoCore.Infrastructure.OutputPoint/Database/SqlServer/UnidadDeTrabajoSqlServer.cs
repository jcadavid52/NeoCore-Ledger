using MediatR;
using NeoCore.Domain.Interfaces;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer
{
    public class UnidadDeTrabajoSqlServer : IUnidadDeTrabajo
    {
        private readonly SqlServerContexto _contexto;
        private readonly IPublisher _publisher;
        private readonly HashSet<ITieneEventosDominio> _agregadosRegistrados = new();

        public UnidadDeTrabajoSqlServer(
            SqlServerContexto contexto,
            IPublisher publisher)
        {
            _contexto = contexto;
            _publisher = publisher;
        }

        public void RegistrarAgregado(ITieneEventosDominio agregado)
            => _agregadosRegistrados.Add(agregado);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var entidadesConEventos = _contexto.ChangeTracker.Entries<ITieneEventosDominio>()
           .Select(e => e.Entity)
           .Concat(_agregadosRegistrados)
           .Distinct()
           .Where(entidad => entidad.ObtenerEventosNoConfirmados.Any())
           .ToList();

            var eventos = entidadesConEventos
            .SelectMany(entidad => entidad.ObtenerEventosNoConfirmados)
            .ToList();

            foreach (var evento in eventos)
            {
                await _publisher.Publish(evento, cancellationToken);
            }

            foreach (var entidad in entidadesConEventos)
            {
                entidad.LimpiarEventosNoConfirmados();
            }

            return await _contexto.SaveChangesAsync(cancellationToken);
        }
    }
}
