namespace GestionBrasserie.Domain;

public class Beer
{
    protected Beer()
    {
    }

    public Beer(Brewery brewery, string name, decimal price, decimal alcoholPercentage)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");

        if (alcoholPercentage > 100)
            throw new ArgumentException("Percentage cannot be > 100");

        Brewery = brewery;
        Name = name;
        Price = price;
        AlcoholPercentage = alcoholPercentage;
        brewery.AddBrewedBeer(this);
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public Brewery Brewery { get; set; }
    public decimal Price { get; set; }
    public decimal AlcoholPercentage { get; set; }
    public virtual ICollection<Reseller> Resellers { get; set; } = new List<Reseller>();
    
    public virtual void AddReseller(Reseller reseller)
    {
        Resellers.Add(reseller);
    }
}