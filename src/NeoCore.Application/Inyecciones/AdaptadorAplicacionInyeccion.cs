using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NeoCore.Application.Comportamientos;

namespace NeoCore.Application.Inyecciones
{
    public static class AdaptadorAplicacionInyeccion
    {
        public static IServiceCollection AgregarApplicationInyeccion(this IServiceCollection servicios)
        {
            servicios.AddTransient(typeof(IPipelineBehavior<,>), typeof(IdempotenciaComportamiento<,>));

            servicios.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(AdaptadorAplicacionInyeccion).Assembly);
            });
            return servicios;
        }
    }
}
