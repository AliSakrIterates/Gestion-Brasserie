using MediatR;

namespace GestionBrasserie.Services.Reseller;

public class AddListedBeerRequest : IRequest<AddListedBeerResponse>
{
    public int ResellerId { get; set; }
    public int BeerId { get; set; }
}

public class AddListedBeerResponse
{
}