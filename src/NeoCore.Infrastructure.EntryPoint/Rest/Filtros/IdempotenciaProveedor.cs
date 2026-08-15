using NeoCore.Application.PuertosEntrada;

namespace NeoCore.Infrastructure.EntryPoint.Rest.Filtros
{
    public class IdempotenciaProveedor : IIdempotenciaProveedor
    {
        private Guid? _clave;

        public Guid? ObtenerClave() => _clave;

        public void EstablecerClave(Guid clave) => _clave = clave;
    }
}
