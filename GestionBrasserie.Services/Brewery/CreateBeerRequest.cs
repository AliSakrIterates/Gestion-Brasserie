using MediatR;

namespace GestionBrasserie.Services.Brewery;

public class CreateBeerRequest : IRequest<CreateBeerResponse>
{
    public long BreweryId { get; set; }
    public BeerDataRequest Beer { get; set; }
}

public class CreateBeerResponse
{
    public int Id { get; set; }
}