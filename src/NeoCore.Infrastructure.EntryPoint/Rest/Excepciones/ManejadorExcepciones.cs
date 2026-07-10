using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeoCore.Domain.Excepciones;

namespace NeoCore.Infrastructure.EntryPoint.Rest.Excepciones
{
    public class ManejadorExcepciones : IExceptionHandler
    {
        private readonly ILogger<ManejadorExcepciones> _logger;

        public ManejadorExcepciones(ILogger<ManejadorExcepciones> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var (statusCode, titulo, detalle) = exception switch
            {
                DominioExcepcion ex => (StatusCodes.Status400BadRequest, "Error de validación", ex.Message),
                ExcepcionNoEncontrado ex => (StatusCodes.Status404NotFound, "Recurso no encontrado", ex.Message),
                ExcepcionConflicto ex => (StatusCodes.Status409Conflict, "Conflicto de concurrencia", ex.Message),
                ExcepcionInfraestructura ex => (StatusCodes.Status500InternalServerError, "Error de infraestructura", ex.Message),
                _ => (StatusCodes.Status500InternalServerError, "Error interno del servidor", "Ocurrió un error inesperado.")
            };

            if (statusCode >= 500)
                _logger.LogError(exception, "Excepción no controlada: {Mensaje}", exception.Message);
            else
                _logger.LogWarning(exception, "Excepción controlada ({StatusCode}): {Mensaje}", statusCode, exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = titulo,
                Detail = detalle,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
