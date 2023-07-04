namespace AdminBaker.Entities;

public class Receta : EntityBase
{
    public required string Nombre { get; set; }
    public required string Detalle { get; set; }
}