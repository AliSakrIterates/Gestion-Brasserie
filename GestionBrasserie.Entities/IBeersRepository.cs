
namespace GestionBrasserie.Domain;

public interface IBeersRepository: IRepository<Beer>
{
    Task<Beer> GetByIdAsync(int requestBeerId, CancellationToken cancellationToken);
    Task DeleteBeerAsync(int requestId, CancellationToken cancellationToken);
}