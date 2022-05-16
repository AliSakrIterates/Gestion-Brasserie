using GestionBrasserie.Domain;
using MediatR;

namespace GestionBrasserie.Services.Brewery;

public class CreateBeerHandler : IRequestHandler<CreateBeerRequest, CreateBeerResponse>
{
    public CreateBeerHandler(IBreweryRepository breweryRepository, IBeersRepository beersRepository)
    {
        BreweryRepository = breweryRepository ?? throw new ArgumentNullException(nameof(breweryRepository));
        BeersRepository = beersRepository ?? throw new ArgumentNullException(nameof(beersRepository));
    }

    public IBreweryRepository BreweryRepository { get; }
    public IBeersRepository BeersRepository { get; }

    public async Task<CreateBeerResponse> Handle(CreateBeerRequest request, CancellationToken cancellationToken)
    {
        var brewery = await BreweryRepository.GetByIdAsync(request.BreweryId, cancellationToken);

        var beer = new Beer(brewery, request.Beer.Name, request.Beer.Price, request.Beer.AlcoholPercentage);

        brewery.AddBrewedBeer(beer);

        await BeersRepository.AddAsync(beer, cancellationToken);

        return new CreateBeerResponse { Id = beer.Id };
    }
}