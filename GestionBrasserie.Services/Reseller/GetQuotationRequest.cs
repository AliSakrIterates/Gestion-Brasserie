using MediatR;

namespace GestionBrasserie.Services.Reseller;

public class GetQuotationRequest : IRequest<GetQuotationResponse>
{
    public int ResellerId { get; set; }
    public List<BeersQuotationRequest> Beers { get; set; }
}

public class BeersQuotationRequest : IEquatable<BeersQuotationRequest>
{
    public int BeerId { get; set; }
    public int Quantity { get; set; }

    public bool Equals(BeersQuotationRequest? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return BeerId == other.BeerId && Quantity == other.Quantity;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BeersQuotationRequest)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(BeerId, Quantity);
    }
}

public class GetQuotationResponse
{
    public decimal Price { get; set; }
}