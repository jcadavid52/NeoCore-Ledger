using Microsoft.Extensions.DependencyInjection;

namespace NeoCore.Application.Inyecciones
{
    public static class AdaptadorAplicacionInyeccion
    {
        public static IServiceCollection AgregarApplicationInyeccion(this IServiceCollection servicios)
        {
            servicios.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(AdaptadorAplicacionInyeccion).Assembly);
            });
            return servicios;
        }
    }
}
