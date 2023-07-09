namespace AdminBaker.Entities.Info;

public class ProductoInfo
{
    public int Id { get; set; }
    public required string TipoTorta { get; set; }
    public int TipoTortaId { get; set; }
    public required string Nombre { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public required string Relleno { get; set; }
    public double Tamanio { get; set; }
    public string? ImagenUrl { get; set; }
    public bool Especial { get; set; }
}