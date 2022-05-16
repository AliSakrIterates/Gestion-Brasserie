namespace GestionBrasserie.Domain;

public interface IRepository<in T> where T: class
{
    Task AddAsync(T entity, CancellationToken cancellationToken);

    Task SaveChanges();
}