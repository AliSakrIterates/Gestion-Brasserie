using GestionBrasserie.Domain;
using MediatR;

namespace GestionBrasserie.Services.Reseller;

public class AddListedBeerHandler : IRequestHandler<AddListedBeerRequest, AddListedBeerResponse>
{
    public AddListedBeerHandler(IResellerRepository resellerRepository, IBeersRepository beersRepository)
    {
        ResellerRepository = resellerRepository ?? throw new ArgumentNullException(nameof(resellerRepository));
        BeersRepository = beersRepository ?? throw new ArgumentNullException(nameof(beersRepository));
    }

    public IResellerRepository ResellerRepository { get; }
    public IBeersRepository BeersRepository { get; }

    public async Task<AddListedBeerResponse> Handle(AddListedBeerRequest request, CancellationToken cancellationToken)
    {
        var reseller = await ResellerRepository.GetByIdAsync(request.ResellerId, cancellationToken);
        if (reseller.BeersStock.Any(x => x.Beer.Id == request.BeerId))
            throw new InvalidOperationException("Beer already exists for this reseller");
        var beer = await BeersRepository.GetByIdAsync(request.BeerId, cancellationToken);

        reseller.AddListedBeer(beer);

        await ResellerRepository.SaveChanges();

        return new AddListedBeerResponse();
    }
}