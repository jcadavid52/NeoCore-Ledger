namespace NeoCore.Infrastructure.OutputPoint.Database.SqlServer.Configuracion
{
    public record BaseDatos
    {
        public SQL SQL { get; init; } = null!;
        public Semilla Semilla { get; set; } = null!;
    }

    public record SQL
    {
        public bool AplicarMigracionEnInicio { get; init; }
        public CadenaConexion CadenaConexion { get; init; } = null!;
    }

    public record CadenaConexion
    {
        public string Coneccion { get; init; } = null!;
    }

    public record Semilla
    {
        public bool AplicarSemillaEnInicio { get; init; }
        public string RutaArchivo { get; init; } = null!;
    }
}
