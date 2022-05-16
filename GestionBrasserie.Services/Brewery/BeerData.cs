namespace GestionBrasserie.Services.Brewery;

public class BeerData
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long BreweryId { get; set; }
    public decimal Price { get; set; }
    public decimal AlcoholPercentage { get; set; }
    public List<long> ResellerIds { get; set; }
}

public class BeerDataRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal AlcoholPercentage { get; set; }
}