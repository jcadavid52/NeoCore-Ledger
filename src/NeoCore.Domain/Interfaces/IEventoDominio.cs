namespace NeoCore.Domain.Interfaces
{
    public interface IEventoDominio
    {
        Guid IdEvento { get; }

        Guid IdAgregado { get; }

        int Version { get; }

        DateTime OcurrioEn { get; }
    }
}
