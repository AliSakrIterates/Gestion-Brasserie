namespace GestionBrasserie.Domain;

public class ObjectNotFoundException : Exception
{
    public ObjectNotFoundException(string s) : base(s)
    {
    }
}