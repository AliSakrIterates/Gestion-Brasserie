using System.ComponentModel.DataAnnotations;
using GestionBrasserie.Services.Reseller;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestionBrasserie.Hosting.Controllers;

[ApiController]
[Route("resellers")]
public class ResellerController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResellerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Add a beer to the list of sold beers by the reseller
    /// </summary>
    /// <param name="id">The Id of the reseller</param>
    /// <param name="beerId">The Id of the beer to add</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id}/beers/{beerId}")]
    public Task<AddListedBeerResponse> AddListedBeer([FromRoute] [Required] int id, [FromRoute] [Required] int beerId,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(new AddListedBeerRequest { ResellerId = id, BeerId = beerId }, cancellationToken);
    }

    /// <summary>
    ///     Update the beer stock for a specific reseller
    /// </summary>
    /// <param name="id">The Id of the reseller</param>
    /// <param name="beerId">The Id of the beer</param>
    /// <param name="quantity">The new quantity</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id}/beers/{beerId}/stock/{quantity}")]
    public Task<UpdateBeerStockResponse> UpdateBeerStock([FromRoute] [Required] int id,
        [FromRoute] [Required] int beerId, [FromRoute] [Required] int quantity, CancellationToken cancellationToken)
    {
        return _mediator.Send(new UpdateBeerStockRequest { Id = id, BeerId = beerId, Quantity = quantity },
            cancellationToken);
    }

    /// <summary>
    ///     Delete a beer from sold beers for a specific reseller
    /// </summary>
    /// <param name="id">The reseller Id</param>
    /// <param name="beerId">The beer Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}/beers/{beerId}")]
    public Task<RemoveListedBeerResponse> RemoveBeer([FromRoute] [Required] int id,
        [FromRoute] [Required] int beerId, CancellationToken cancellationToken)
    {
        return _mediator.Send(new RemoveListedBeerRequest { Id = id, BeerId = beerId }, cancellationToken);
    }

    /// <summary>
    ///     Get a quotation price
    /// </summary>
    /// <param name="request">The request containing the list of beers and the quantity</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("/quote")]
    public Task<GetQuotationResponse> GetQuotation([FromBody] [Required] GetQuotationRequest request,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(request, cancellationToken);
    }
}