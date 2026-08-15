using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using NeoCore.Infrastructure.EntryPoint.Rest.Excepciones;
using NeoCore.Infrastructure.EntryPoint.Rest.Filtros;
using NeoCore.Application.PuertosEntrada;

namespace NeoCore.Infrastructure.EntryPoint.Inyecciones
{
    public static class RestInyeccion
    {
        public static IServiceCollection AgregarRestInyeccion(this IServiceCollection servicios)
        {
            servicios.AddHttpContextAccessor();
            servicios.AddScoped<IIdempotenciaProveedor, IdempotenciaProveedor>();
            servicios.AddScoped<FiltroIdempotencia>();

            servicios.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            servicios.AddExceptionHandler<ManejadorExcepciones>();
            servicios.AddProblemDetails();

            return servicios;
        }
    }
}
