using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using NeoCore.Application.PuertosEntrada;

namespace NeoCore.Infrastructure.EntryPoint.Rest.Filtros
{
    public class FiltroIdempotencia : IAsyncActionFilter
    {
        private const string NombreHeader = "Idempotency-Key";

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(NombreHeader, out var valor)
                || !Guid.TryParse(valor.ToString(), out var clave))
            {
                context.Result = new BadRequestObjectResult(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Header requerido",
                    Detail = "El header 'Idempotency-Key' es obligatorio y debe ser un GUID válido."
                });
                return;
            }

            var proveedor = context.HttpContext.RequestServices
                .GetRequiredService<IIdempotenciaProveedor>();

            proveedor.EstablecerClave(clave);

            await next();
        }
    }
}
