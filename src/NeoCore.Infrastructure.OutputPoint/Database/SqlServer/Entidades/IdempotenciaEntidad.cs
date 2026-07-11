namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Entidades
{
    public record IdempotenciaEntidad
    {
        public Guid IdClave { get; init; }
        public string NombreComando { get; init; } = string.Empty;
        public DateTime CreadoEn { get; init; } = DateTime.UtcNow;
    }
}
