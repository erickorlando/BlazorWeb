namespace AdminBaker.Entities;

public class Producto : EntityBase
{
    public required TipoTorta TipoTorta { get; set; }
    public int TipoTortaId { get; set; }
    public required string Nombre { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public required string Relleno { get; set; }
    public double Tamanio { get; set; }
}