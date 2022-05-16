namespace GestionBrasserie.Domain;

public class Brewery
{
    protected Brewery()
    {
    }

    public Brewery(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("Name cannot be empty");
        Name = name;
    }

    public long Id { get; set; }
    public virtual ICollection<Beer> BrewedBeers { get; set; } = new List<Beer>();

    public string Name { get; set; }

    public void AddBrewedBeer(Beer beer)
    {
        BrewedBeers.Add(beer);
    }
}