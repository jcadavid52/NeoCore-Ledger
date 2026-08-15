namespace NeoCore.Application.PuertosEntrada
{
    public interface IIdempotenciaProveedor
    {
        Guid? ObtenerClave();
        void EstablecerClave(Guid clave);
    }
}
