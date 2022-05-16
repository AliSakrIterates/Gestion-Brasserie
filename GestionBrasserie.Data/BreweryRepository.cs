using GestionBrasserie.Domain;
using Microsoft.EntityFrameworkCore;

namespace GestionBrasserie.Data;

public class BreweryRepository : IBreweryRepository
{
    public GestionBrasserieContext Context { get; }

    public BreweryRepository(GestionBrasserieContext context) 
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Brewery> GetByIdAsync(long requestBreweryId, CancellationToken cancellationToken)
    {
        var result = await Context.Breweries.Include(x => x.BrewedBeers).ThenInclude(x => x.Resellers).Where(b => b.Id == requestBreweryId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (result is null)
            throw new ObjectNotFoundException($"cannot find brewery with id {requestBreweryId}");

        return result;
    }

    public async Task AddAsync(Brewery entity, CancellationToken cancellationToken)
    {
        await Context.Breweries.AddAsync(entity, cancellationToken);
        // Ideally we should use a UnitOfWork pattern with a scoped Middleware that commits the transaction when request is done
        await Context.SaveChangesAsync(cancellationToken);
    }
    
    public Task SaveChanges()
    {
        return Context.SaveChangesAsync();
    }
}