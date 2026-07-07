using NeoCore.Domain.Interfaces;
using NeoCore.Domain.Repositorios;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Entidades;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Repositorios
{
    public class EventoAlmacenRepositorio : IEventoAlmacenRepositorio
    {
        public async Task AgregarAsync(Guid idAgregado, IReadOnlyCollection<IEventoDominio> eventos, int versionEsperada, CancellationToken cancellationToken)
        {
            if (eventos == null || !eventos.Any()) return;

            var ultimaVersion = SeedDatabase.ObtenerEventos()
                .Where(x => x.IdAgregado == idAgregado)
                .Select(x => x.Version)
                .DefaultIfEmpty(0)
                .Max();

            if (ultimaVersion != versionEsperada)
            {
                throw new Exception($"Conflicto de concurrencia: La versión esperada era {versionEsperada}, pero la última versión en base de datos es {ultimaVersion}.");
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

                SeedDatabase.AgregarEvento(nuevoEventoGuardado);
            }

            await Task.CompletedTask;
        }

        public async Task<IReadOnlyCollection<IEventoDominio>> CargarAsync(Guid IdAgregado, CancellationToken cancellationToken)
        {
            var eventosGuardados = SeedDatabase.ObtenerEventos()
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
                    throw new Exception($"El tipo de evento {eventoGuardado.TipoMensaje} no se pudo resolver.");
                }
            }

            return eventosDominio;
        }
    }
}
