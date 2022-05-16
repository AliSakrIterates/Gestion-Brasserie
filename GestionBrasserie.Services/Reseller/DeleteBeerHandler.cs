using GestionBrasserie.Domain;
using MediatR;

namespace GestionBrasserie.Services.Reseller;

public class DeleteBeerHandler : IRequestHandler<DeleteBeerRequest, DeleteBeerResponse>
{
    public DeleteBeerHandler(IBeersRepository beersRepository)
    {
        BeersRepository = beersRepository ?? throw new ArgumentNullException(nameof(beersRepository));
    }

    public IBeersRepository BeersRepository { get; }

    public async Task<DeleteBeerResponse> Handle(DeleteBeerRequest request, CancellationToken cancellationToken)
    {
        await BeersRepository.DeleteBeerAsync(request.Id, cancellationToken);

        await BeersRepository.SaveChanges();
        return new DeleteBeerResponse();
    }
}