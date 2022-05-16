namespace GestionBrasserie.Domain;

public class BeerStock
{
    protected BeerStock()
    {
    }

    public BeerStock(Beer beer, int quantity)
    {
        Beer = beer;
        Quantity = quantity;
    }

    public Beer Beer { get; set; }
    public int Quantity { get; set; }
    public int Id { get; set; }

    public void SetQuantity(int requestQuantity)
    {
        Quantity = requestQuantity;
    }
}