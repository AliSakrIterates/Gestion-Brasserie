using MediatR;

namespace GestionBrasserie.Services.Reseller;

public class DeleteBeerRequest : IRequest<DeleteBeerResponse>
{
    public int Id { get; set; }
}

public class DeleteBeerResponse
{
}