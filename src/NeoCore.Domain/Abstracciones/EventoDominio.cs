using NeoCore.Domain.Interfaces;

namespace NeoCore.Domain.Abstracciones
{
    public abstract record EventoDominio : IEventoDominio
    {
        public Guid IdEvento { get; init; } = Guid.NewGuid();

        public Guid IdAgregado { get; init; }

        public int Version { get; init; }

        public DateTime OcurrioEn { get; init; } = DateTime.Now;
    }
}
