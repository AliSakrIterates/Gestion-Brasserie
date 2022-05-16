using GestionBrasserie.Domain;
using Microsoft.EntityFrameworkCore;

namespace GestionBrasserie.Data;

public class ResellerRepository : IResellerRepository
{
    public GestionBrasserieContext Context { get; }

    public ResellerRepository(GestionBrasserieContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Reseller> GetByIdAsync(int requestResellerId, CancellationToken cancellationToken)
    {
        var result = await Context.Resellers.Include(x => x.BeersStock)
            .ThenInclude(x => x.Beer).Where(b => b.Id == requestResellerId).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (result is null)
            throw new ObjectNotFoundException($"cannot find reseller with id {requestResellerId}");

        return result;
        
    }

    public async Task Remove(int requestId, int requestBeerId)
    {
        var result = await Context.Resellers.Include(x => x.BeersStock)
           .ThenInclude(x => x.Beer).Where(b => b.Id == requestId).FirstOrDefaultAsync();

        if (result is null)
            throw new ObjectNotFoundException($"cannot find reseller with id {requestId}");
        Context.BeerStocks.Remove(result.BeersStock.FirstOrDefault(x => x.Beer.Id == requestBeerId));

    }

    public async Task AddAsync(Reseller entity, CancellationToken cancellationToken)
    {
        await Context.Resellers.AddAsync(entity, cancellationToken);
        // Ideally we should use a UnitOfWork pattern with a scoped Middleware that commits the transaction when request is done
        await Context.SaveChangesAsync(cancellationToken);
    }

    public Task SaveChanges()
    {
        return Context.SaveChangesAsync();
    }
}