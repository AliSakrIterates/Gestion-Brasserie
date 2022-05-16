using GestionBrasserie.Domain;
using MediatR;

namespace GestionBrasserie.Services.Brewery;

public class GetBeersHandler : IRequestHandler<GetBeersRequest, GetBeersResponse>
{
    public GetBeersHandler(IBreweryRepository breweryRepository)
    {
        BreweryRepository = breweryRepository ?? throw new ArgumentNullException(nameof(breweryRepository));
    }

    public IBreweryRepository BreweryRepository { get; }

    public async Task<GetBeersResponse> Handle(GetBeersRequest request, CancellationToken cancellationToken)
    {
        // Ideally we should go through a data access directly (for example using dapper) to avoid using the repository (which is heavy)
        // And to avoid unnecessary mapping from Domain to DTO.
        // This will also implement the CQRS pattern
        var brewery = await BreweryRepository.GetByIdAsync(request.Id, cancellationToken);

        return new GetBeersResponse
        {
            Beers = brewery.BrewedBeers.Select(b => new BeerData
            {
                Id = b.Id,
                Name = b.Name,
                Price = b.Price,
                AlcoholPercentage = b.AlcoholPercentage,
                BreweryId = b.Brewery.Id,
                ResellerIds = b.Resellers.Select(r => r.Id).ToList()
            }).ToList()
        };
    }
}