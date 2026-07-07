using Microsoft.Extensions.DependencyInjection;
using NeoCore.Domain.Repositorios;
using NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Repositorios;

namespace NeoCore.Infrastructure.OutputPoint.Inyecciones
{
    public static class AdaptadorPuntoSalidaInyeccion
    {
        public static IServiceCollection AgregarAdaptadorPuntoSalida(this IServiceCollection servicios)
        {
            servicios.AddScoped<ICuentaContableRepositorio, CuentaContableRepositorio>();
            servicios.AddScoped<IEventoAlmacenRepositorio, EventoAlmacenRepositorio>();

            return servicios;
        }
    }
}
