using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeoCore.Application.PuertosSalida;
using NeoCore.Domain.Repositorios;
using NeoCore.Infrastructure.EntryPoint.Mensajería.RabbitMQ.Consumidores;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Configuracion;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Repositorios;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Servicios;
using NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.Estado;
using NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.MaquinasDeEstado;
using NeoCore.Infrastructure.OutputPoint.Mensajeria.RabbitMQ.Publicadores;
using NeoCore.Infrastructure.OutputPoint.Rest.Clientes.CuentasServicio;
using Polly;
using Polly.Extensions.Http;

namespace NeoCore.Infrastructure.OutputPoint.Inyecciones
{
    public static class AdaptadorPuntoSalidaInyeccion
    {
        public static IServiceCollection AgregarAdaptadorPuntoSalida(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AddScoped<ILibroContableRepositorio, LibroContableRepositorio>();
            servicios.AddScoped<IEventoAlmacenRepositorio, EventoAlmacenRepositorio>();
            servicios.AddScoped<IIdempotenciaRepositorio, IdempotenciaRepositorio>();
            servicios.AddScoped<ITransferenciaRepositorio, TransferenciaRepositorio>();
            servicios.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajoSqlServer>();

            ConfiguracionSqlServer(servicios, configuracion);
            ConfiguracionMassTransit(servicios, configuracion);
            ConfiguracionClienteRest(servicios, configuracion);
            ConfiguracionMediatR(servicios);

            return servicios;
        }

        private static void ConfiguracionSqlServer(IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.Configure<BaseDatos>(configuracion.GetSection("BaseDatos"));

            var seccion = configuracion.GetSection("BaseDatos").Get<BaseDatos>()
                ?? throw new ArgumentNullException("Error al obtener la configuración de sql server");

            var connectionString = seccion.SQL.CadenaConexion.Coneccion
                ?? throw new ArgumentNullException("Error al obtener cadena de conexión");

            servicios.AddDbContext<SqlServerContexto>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            if (seccion.SQL.AplicarMigracionEnInicio)
            {
                servicios.AddHostedService<BaseDatosMigracionServicio>();
            }

            if (seccion.Semilla.AplicarSemillaEnInicio)
            {
                servicios.AddHostedService<BaseDatosSemillaServicio>();
            }
        }

        private static void ConfiguracionMassTransit(IServiceCollection servicios, IConfiguration configuracion)
        {
            var rabbitConfig = configuracion.GetSection("RabbitMQ").Get<RabbitMQConfig>()
                ?? throw new ArgumentNullException("Error al obtener la configuración de RabbitMQ");

            servicios.AddMassTransit(x =>
            {
                x.AddSagaStateMachine<TransferenciaEstadoMaquina, TransferenciaEstado>()
                    .EntityFrameworkRepository(r =>
                    {
                        r.ExistingDbContext<SqlServerContexto>();
                        r.UseSqlServer();
                    });

                x.AddConsumer<BloquearSaldoConsumidor>();
                x.AddConsumer<AcreditarSaldoConsumidor>();
                x.AddConsumer<LiquidarSaldoConsumidor>();
                x.AddConsumer<NotificarTransferenciaConsumidor>();

                x.AddEntityFrameworkOutbox<SqlServerContexto>(o =>
                {
                    o.QueryDelay = TimeSpan.FromSeconds(1);
                    o.UseSqlServer();
                    o.UseBusOutbox();
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitConfig.Host, rabbitConfig.VirtualHost, h =>
                    {
                        h.Username(rabbitConfig.Username);
                        h.Password(rabbitConfig.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        private static void ConfiguracionClienteRest(IServiceCollection servicios, IConfiguration configuracion)
        {
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            var circuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));

            servicios.AddHttpClient<ICuentaServicioCliente, ServicioCuentaCliente>(client =>
            {
                var baseUrl = configuracion["AccountService:BaseUrl"] ?? "http://localhost:5000";
                client.BaseAddress = new Uri(baseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddPolicyHandler(circuitBreakerPolicy)
            .AddPolicyHandler(retryPolicy);
        }

        private static void ConfiguracionMediatR(IServiceCollection servicios)
        {
            servicios.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(AdaptadorPuntoSalidaInyeccion).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(TransferenciaIniciadaPublicador).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(BloquearSaldoExistosoPublicador).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(LiquidarSaldoExistosoPublicador).Assembly);
                configuration.RegisterServicesFromAssembly(typeof(AcreditarSaldoExitosoPublicador).Assembly);
            });
        }
    }

    public class RabbitMQConfig
    {
        public string Host { get; set; } = "localhost";
        public string VirtualHost { get; set; } = "/";
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
    }
}
