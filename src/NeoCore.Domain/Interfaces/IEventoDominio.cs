namespace NeoCore.Domain.Interfaces
{
    public interface IEventoDominio
    {
        Guid IdEvento { get; }

        Guid IdAgregado { get; }

        int Version { get; }

        //public string TipoMensaje { get; }

        //public string Dato { get; }

        DateTime OcurrioEn { get; }
    }
}
