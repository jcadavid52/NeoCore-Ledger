namespace NeoCore.Application.Interfaces
{
    public interface IIdempotenciaProveedor
    {
        Guid? ObtenerClave();
        void EstablecerClave(Guid clave);
    }
}
