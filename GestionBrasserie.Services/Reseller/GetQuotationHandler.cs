using GestionBrasserie.Domain;
using MediatR;

namespace GestionBrasserie.Services.Reseller;

public class GetQuotationHandler : IRequestHandler<GetQuotationRequest, GetQuotationResponse>
{
    public GetQuotationHandler(IResellerRepository resellerRepository)
    {
        ResellerRepository = resellerRepository ?? throw new ArgumentNullException(nameof(resellerRepository));
    }

    public IResellerRepository ResellerRepository { get; }

    public async Task<GetQuotationResponse> Handle(GetQuotationRequest request, CancellationToken cancellationToken)
    {
        // Ideally this should be done in a Domain object where we would encapsulate the business logic and save the quotation
        // In the database, but this is out of scope of the project
        if (!request.Beers.Any())
            throw new InvalidOperationException("order cannot be empty");

        if (request.Beers.Count != request.Beers.Distinct().Count())
            throw new InvalidOperationException("request contains duplicates");

        var reseller = await ResellerRepository.GetByIdAsync(request.ResellerId, cancellationToken);

        var requestedBeerIds = request.Beers.Select(x => x.BeerId);
        var requestedBeers = reseller.BeersStock.Where(b => requestedBeerIds.Contains(b.Beer.Id));

        if (requestedBeers.Count() < requestedBeerIds.Count())
            throw new InvalidOperationException("some beers are not sold by the reseller");

        decimal price = 0;
        var numberOfItems = 0;
        foreach (var requestedBeer in request.Beers)
        {
            var beer = requestedBeers.Single(x => x.Beer.Id == requestedBeer.BeerId);

            if (beer.Quantity < requestedBeer.Quantity)
                throw new InvalidOperationException("no enough stock for a beer");

            price += requestedBeer.Quantity * beer.Beer.Price;
            numberOfItems += requestedBeer.Quantity;
        }

        if (numberOfItems > 20)
            price = price / (decimal)1.2;
        else if (numberOfItems > 10)
            price = price / (decimal)1.1;

        return new GetQuotationResponse { Price = price };
    }
}