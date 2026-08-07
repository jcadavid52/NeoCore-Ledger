using MediatR;

namespace NeoCore.Domain.Interfaces
{
    public interface IEventoDominio : INotification
    {
        Guid IdEvento { get; }

        Guid IdAgregado { get; }

        int Version { get; }

        DateTime OcurrioEn { get; }
    }
}
