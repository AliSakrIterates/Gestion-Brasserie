using MediatR;

namespace GestionBrasserie.Services.Reseller;

public class UpdateBeerStockRequest : IRequest<UpdateBeerStockResponse>
{
    public int Id { get; set; }
    public int BeerId { get; set; }
    public int Quantity { get; set; }
}

public class UpdateBeerStockResponse
{
}