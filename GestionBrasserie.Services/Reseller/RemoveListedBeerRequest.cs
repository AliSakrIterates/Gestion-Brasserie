using MediatR;

namespace GestionBrasserie.Services.Reseller;

public class RemoveListedBeerRequest : IRequest<RemoveListedBeerResponse>
{
    public int Id { get; set; }
    public int BeerId { get; set; }
}

public class RemoveListedBeerResponse
{
}