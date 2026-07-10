using NeoCore.Domain.Excepciones;
using NeoCore.Domain.Interfaces;
using NeoCore.Domain.Repositorios;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Entidades;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Repositorios
{
    public class EventoAlmacenRepositorio : IEventoAlmacenRepositorio
    {
        private readonly SqlServerContexto _contexto;

        public EventoAlmacenRepositorio(SqlServerContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task AgregarAsync(Guid idAgregado, IReadOnlyCollection<IEventoDominio> eventos, int versionEsperada, CancellationToken cancellationToken)
        {
            if (eventos == null || !eventos.Any()) return;

            var ultimaVersion = _contexto.EventoAlmacenEntidad
               .Where(x => x.IdAgregado == idAgregado)
               .Select(x => (int?)x.Version)
               .DefaultIfEmpty()
               .Max() ?? 0;

            if (ultimaVersion != versionEsperada)
            {
                throw new ExcepcionConflicto($"Conflicto de concurrencia: La versión esperada era {versionEsperada}, pero la última versión en base de datos es {ultimaVersion}.");
            }

            var versionActual = versionEsperada;

            foreach (var eventoDominio in eventos)
            {
                versionActual++;

                var jsonNode = JsonSerializer.SerializeToNode(eventoDominio, eventoDominio.GetType())?.AsObject();
                if (jsonNode == null) continue;

                jsonNode["Version"] = versionActual;
                jsonNode["IdAgregado"] = idAgregado.ToString();

                var nuevoEventoGuardado = new EventoAlmacenEntidad
                {
                    IdAgregado = idAgregado,
                    Version = versionActual,
                    IdEvento = eventoDominio.IdEvento,
                    OcurrioEn = eventoDominio.OcurrioEn,
                    TipoMensaje = eventoDominio.GetType().Name,
                    Dato = jsonNode.ToJsonString()
                };

                _contexto.EventoAlmacenEntidad.Add(nuevoEventoGuardado);
            }

            await _contexto.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<IEventoDominio>> CargarAsync(Guid IdAgregado, CancellationToken cancellationToken)
        {
            var eventosGuardados = _contexto.EventoAlmacenEntidad
                .Where(x => x.IdAgregado == IdAgregado)
                .OrderBy(x => x.Version);

            var eventosDominio = new List<IEventoDominio>();

            foreach (var eventoGuardado in eventosGuardados)
            {
                var tipoEvento = Type.GetType($"NeoCore.Domain.Eventos.{eventoGuardado.TipoMensaje}, NeoCore.Domain");

                if (tipoEvento != null)
                {
                    var jsonNode = JsonNode.Parse(eventoGuardado.Dato)?.AsObject();
                    if (jsonNode == null) continue;

                    jsonNode["Version"] = eventoGuardado.Version;
                    jsonNode["IdAgregado"] = eventoGuardado.IdAgregado.ToString();
                    jsonNode["IdEvento"] = eventoGuardado.IdEvento.ToString();
                    jsonNode["OcurrioEn"] = eventoGuardado.OcurrioEn;

                    var evento = (IEventoDominio)JsonSerializer.Deserialize(jsonNode.ToJsonString(), tipoEvento);
                    if (evento != null)
                    {
                        eventosDominio.Add(evento);
                    }
                }
                else
                {
                    throw new ExcepcionInfraestructura($"El tipo de evento {eventoGuardado.TipoMensaje} no se pudo resolver.");
                }
            }

            return eventosDominio;
        }
    }
}
