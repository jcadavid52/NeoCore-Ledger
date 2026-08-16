namespace NeoCore.Application.PuertosSalida.Respuestas
{
    public record ValidarCuentaRespuesta(
        string TipoCuenta,
        string NumeroCuenta,
        string Estado,
        UsuarioCuentaRespuesta UsuarioCuente);
}
