using MediatR;

namespace GestionBrasserie.Services.Brewery;

public class GetBeersRequest : IRequest<GetBeersResponse>
{
    public int Id { get; set; }
}

public class GetBeersResponse
{
    public List<BeerData> Beers { get; set; }
}