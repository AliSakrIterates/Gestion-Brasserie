using System.ComponentModel.DataAnnotations;
using GestionBrasserie.Data;
using GestionBrasserie.Services.Brewery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestionBrasserie.Hosting.Controllers;

[ApiController]
[Route("breweries")]
public class BreweryController : ControllerBase
{
    private readonly IMediator _mediator;
    public BreweryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Return the list of the Brewed beers
    /// </summary>
    /// <param name="id">the Id of the brewery</param>
    /// <param name="token"></param>
    /// <returns>An object containing the brewed beers</returns>
    [HttpGet("{id}/beers")]
    public Task<GetBeersResponse> GetBeers([FromRoute] [Required] int id, CancellationToken token)
    {
        return _mediator.Send(new GetBeersRequest { Id = id }, token);
    }

    /// <summary>
    ///     Add a new brewed beer
    /// </summary>
    /// <param name="id">The Id of the brewery</param>
    /// <param name="beer">The beer data</param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost("{id}/beers")]
    public Task<CreateBeerResponse> AddBeer([FromRoute] int id, [FromBody] [Required] BeerDataRequest beer,
        CancellationToken token)
    {
        return _mediator.Send(new CreateBeerRequest { BreweryId = id, Beer = beer }, token);
    }
}