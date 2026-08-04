namespace NeoCore.Application.CasosDeUso.LibroContable.Comandos.Resultados
{
    public record OperacionResultado(
        bool Exitoso,
        string MotivoError);
}
