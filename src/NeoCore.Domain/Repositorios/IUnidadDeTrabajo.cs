namespace NeoCore.Domain.Repositorios
{
    public interface IUnidadDeTrabajo
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
