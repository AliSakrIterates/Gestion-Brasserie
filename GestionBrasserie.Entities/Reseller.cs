namespace GestionBrasserie.Domain;

public class Reseller
{

    public ICollection<BeerStock> BeersStock { get; } = new List<BeerStock>();
    public string Name { get; set; }
    public long Id { get; set; }

    protected Reseller()
    {
    }

    public Reseller(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");
        Name = name;
    }
    
    public Reseller AddListedBeer(Beer beer)
    {
        BeersStock.Add(new BeerStock(beer, 0));
        beer.AddReseller(this);
        return this;
    }
}