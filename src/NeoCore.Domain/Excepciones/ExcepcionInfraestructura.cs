namespace NeoCore.Domain.Excepciones
{
    public class ExcepcionInfraestructura(string message, Exception? innerException = null)
        : Exception(message, innerException)
    {
    }
}
