namespace AdminBaker.Entities;

public class EntityBase
{
    public int Id { get; set; }
    public bool Estado { get; set; }
    public string? Usuario { get; set; }
    protected EntityBase()
    {
        Estado = true;
    }
}