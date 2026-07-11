using MediatR;
using NeoCore.Application.Interfaces;
using NeoCore.Domain.Repositorios;

namespace NeoCore.Application.Comportamientos
{
    public class IdempotenciaComportamiento<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IComandoIdempotente
    {
        private readonly IIdempotenciaRepositorio _repositorio;
        private readonly IIdempotenciaProveedor _proveedor;

        public IdempotenciaComportamiento(
            IIdempotenciaRepositorio repositorio,
            IIdempotenciaProveedor proveedor)
        {
            _repositorio = repositorio;
            _proveedor = proveedor;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var clave = _proveedor.ObtenerClave();

            if (clave is null)
                return await next();

            if (await _repositorio.ExisteAsync(clave.Value, cancellationToken))
                return default!;

            var respuesta = await next();

            await _repositorio.AgregarAsync(
                clave.Value,
                typeof(TRequest).Name,
                cancellationToken);

            return respuesta;
        }
    }
}
