namespace GestionBrasserie.Domain;

public interface IResellerRepository : IRepository<Reseller>
{
    Task<Reseller> GetByIdAsync(int requestResellerId, CancellationToken cancellationToken);
    Task Remove(int requestId, int requestBeerId);
}