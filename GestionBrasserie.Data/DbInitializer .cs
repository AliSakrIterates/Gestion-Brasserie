using GestionBrasserie.Domain;

namespace GestionBrasserie.Data;

public static class DbInitializer
{
    public static void Initialize(GestionBrasserieContext context)
    {
        context.Database.EnsureCreated();

        // Look for any beers.
        if (context.Beers.Any()) return; // DB has been seeded
        
        var breweries = new[]
        {
            new Brewery("Brewery1"),
            new Brewery("Brewery2"),
            new Brewery("Brewery3")
        };
        foreach (var brewery in breweries) context.Breweries.Add(brewery);

        var beers = new[]
        {
            new Beer(breweries[0], "Beer1", (decimal)2.3, 4),
            new Beer(breweries[1],"Beer2", (decimal)2.5, 4),
            new Beer(breweries[2],"Beer3", (decimal)2.6, 4)
        };
        foreach (var beer in beers) context.Beers.Add(beer);
        
        var resellers = new[]
        {
            new Reseller("Reseller1").AddListedBeer(beers[0]),
            new Reseller("Reseller2").AddListedBeer( beers[1]),
            new Reseller("Reseller3").AddListedBeer(beers[2])
        };
        foreach (var reseller in resellers) context.Resellers.Add(reseller);
        context.SaveChanges();
    }
}