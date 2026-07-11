using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using NeoCore.Application.Interfaces;
using NeoCore.Infrastructure.EntryPoint.Rest.Excepciones;
using NeoCore.Infrastructure.EntryPoint.Rest.Filtros;

namespace NeoCore.Infrastructure.EntryPoint.Inyecciones
{
    public static class RestInyeccion
    {
        public static IServiceCollection AgregarRestInyeccion(this IServiceCollection servicios)
        {
            servicios.AddHttpContextAccessor();
            servicios.AddScoped<IIdempotenciaProveedor, IdempotenciaProveedor>();

            servicios.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }).AddMvcOptions(options =>
            {
                options.Filters.Add<FiltroIdempotencia>();
            });

            servicios.AddExceptionHandler<ManejadorExcepciones>();
            servicios.AddProblemDetails();

            return servicios;
        }
    }
}
