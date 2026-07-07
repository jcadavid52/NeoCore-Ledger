using NeoCore.Domain.Abstracciones;

namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Entidades
{
    public record EventoAlmacenEntidad : EventoDominio
    {
        public string TipoMensaje { get; init; } = string.Empty;

        public string Dato { get; init; } = string.Empty;
    }
}
