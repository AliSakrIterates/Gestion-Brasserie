using GestionBrasserie.Domain;
using Microsoft.EntityFrameworkCore;

namespace GestionBrasserie.Data;

public class BeersRepository : IBeersRepository
{
    public GestionBrasserieContext Context { get; }

    public BeersRepository(GestionBrasserieContext context) 
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task DeleteBeerAsync(int requestId, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(requestId, cancellationToken);
        Context.Beers.Remove(entity);
    }

    public async Task<Beer> GetByIdAsync(int requestBeerId, CancellationToken cancellationToken)
    {
        var result = await Context.Beers.Include(x => x.Resellers).ThenInclude(x => x.BeersStock).Where(b => b.Id == requestBeerId).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (result is null)
            throw new ObjectNotFoundException($"cannot find brewery with id {requestBeerId}");

        return result;    
    }

    public async Task AddAsync(Beer entity, CancellationToken cancellationToken)
    {
        await Context.Beers.AddAsync(entity, cancellationToken);
        // Ideally we should use a UnitOfWork pattern with a scoped Middleware that commits the transaction when request is done
        await Context.SaveChangesAsync(cancellationToken);
    }
    
    public Task SaveChanges()
    {
        return Context.SaveChangesAsync();
    }
    
}