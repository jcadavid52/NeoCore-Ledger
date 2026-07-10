using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeoCore.Domain.Repositorios;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Configuracion;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Repositorios;

namespace NeoCore.Infrastructure.OutputPoint.Inyecciones
{
    public static class AdaptadorPuntoSalidaInyeccion
    {
        public static IServiceCollection AgregarAdaptadorPuntoSalida(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AddScoped<ICuentaContableRepositorio, CuentaContableRepositorio>();
            servicios.AddScoped<IEventoAlmacenRepositorio, EventoAlmacenRepositorio>();

            ConfiguracionSqlServer(servicios, configuracion);

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
    }
}
