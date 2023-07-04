namespace AdminBaker.Shared.Response;

public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public int TipoTortaId { get; set; }
    public required string TipoTorta { get; set; }
    public required string Relleno { get; set; }
    public double Tamanio { get; set; }
}