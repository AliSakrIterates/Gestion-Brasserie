namespace GestionBrasserie.Domain;

public interface IBreweryRepository : IRepository<Brewery>
{
    Task<Brewery> GetByIdAsync(long requestBreweryId, CancellationToken cancellationToken);
}