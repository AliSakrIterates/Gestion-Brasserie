using GestionBrasserie.Domain;
using MediatR;

namespace GestionBrasserie.Services.Reseller;

public class RemoveListedBeerHandler : IRequestHandler<RemoveListedBeerRequest, RemoveListedBeerResponse>
{
    public IResellerRepository ResellerRepository { get; }

    public RemoveListedBeerHandler(IResellerRepository resellerRepository)
    {
        ResellerRepository = resellerRepository ?? throw new ArgumentNullException(nameof(resellerRepository));
    }

    public async Task<RemoveListedBeerResponse> Handle(RemoveListedBeerRequest request, CancellationToken cancellationToken)
    {
        await ResellerRepository.Remove(request.Id, request.BeerId);

        return new();
    }
}