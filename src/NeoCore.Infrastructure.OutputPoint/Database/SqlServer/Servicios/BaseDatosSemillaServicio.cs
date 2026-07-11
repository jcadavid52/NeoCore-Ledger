using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NeoCore.Domain.Eventos;
using NeoCore.Domain.Interfaces;
using NeoCore.Domain.Repositorios;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Configuracion;
using System.Text.Json;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Servicios
{
    public class BaseDatosSemillaServicio : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BaseDatosSemillaServicio> _logger;

        public BaseDatosSemillaServicio(
            IServiceProvider serviceProvider,
            IConfiguration configuration,
            ILogger<BaseDatosSemillaServicio> logger)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var seccion = _configuration.GetSection("BaseDatos").Get<BaseDatos>()
                ?? throw new ArgumentNullException("Error al obtener la configuración de sql server");

            if (!seccion.Semilla.AplicarSemillaEnInicio)
                return;

            using var alcance = _serviceProvider.CreateScope();
            var repositorio = alcance.ServiceProvider.GetRequiredService<IEventoAlmacenRepositorio>();

            var ruta = seccion.Semilla.RutaArchivo
                ?? "SeedData/seed-data.json";

            if (!File.Exists(ruta))
            {
                _logger.LogWarning("Archivo de semilla no encontrado: {Ruta}", ruta);
                return;
            }

            var json = await File.ReadAllTextAsync(ruta, cancellationToken);
            var semilla = JsonSerializer.Deserialize<SemillaDto>(json);

            if (semilla?.Eventos == null || semilla.Eventos.Count == 0)
            {
                _logger.LogWarning("El archivo de semilla no contiene eventos");
                return;
            }

            foreach (var eventoDto in semilla.Eventos)
            {
                var idAgregado = Guid.Parse(eventoDto.IdAgregado);

                var eventosExistentes = await repositorio.CargarAsync(idAgregado, cancellationToken);
                if (eventosExistentes.Count > 0)
                {
                    _logger.LogInformation("El agregado {IdAgregado} ya tiene eventos, se omite la semilla", idAgregado);
                    continue;
                }

                var eventoDominio = CrearEventoDominio(eventoDto);

                await repositorio.AgregarAsync(
                    idAgregado,
                    new[] { eventoDominio },
                    versionEsperada: 0,
                    cancellationToken);

                _logger.LogInformation("Semilla aplicada para agregado {IdAgregado}", idAgregado);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private static IEventoDominio CrearEventoDominio(EventoDto dto)
        {
            var idAgregado = Guid.Parse(dto.IdAgregado);

            return dto.Tipo switch
            {
                "DepositarDinero" => new DepositarDinero(dto.Monto, idAgregado, Guid.Parse(dto.IdCuentaOrigen)),
                _ => throw new InvalidOperationException($"Tipo de evento desconocido: {dto.Tipo}")
            };
        }
    }

    internal record SemillaDto
    {
        public List<EventoDto> Eventos { get; init; } = [];
    }

    internal record EventoDto
    {
        public string Tipo { get; init; } = string.Empty;
        public string IdAgregado { get; init; } = string.Empty;
        public decimal Monto { get; init; }
        public string IdCuentaOrigen { get; init; } = "00000000-0000-0000-0000-000000000000";
    }
}
