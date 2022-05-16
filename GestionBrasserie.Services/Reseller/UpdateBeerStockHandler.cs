using GestionBrasserie.Domain;
using MediatR;

namespace GestionBrasserie.Services.Reseller;

public class UpdateBeerStockHandler : IRequestHandler<UpdateBeerStockRequest, UpdateBeerStockResponse>
{
    public UpdateBeerStockHandler(IResellerRepository resellerRepository)
    {
        ResellerRepository = resellerRepository ?? throw new ArgumentNullException(nameof(resellerRepository));
    }

    public IResellerRepository ResellerRepository { get; }

    public async Task<UpdateBeerStockResponse> Handle(UpdateBeerStockRequest request,
        CancellationToken cancellationToken)
    {
        var reseller = await ResellerRepository.GetByIdAsync(request.Id, cancellationToken);
        var beerStock = reseller.BeersStock.FirstOrDefault(x => x.Beer.Id == request.BeerId);
        if (beerStock == null)
            throw new InvalidOperationException("Cannot find beer in reseller stock");

        beerStock.SetQuantity(request.Quantity);

        await ResellerRepository.SaveChanges();
        
        return new UpdateBeerStockResponse();
    }
}