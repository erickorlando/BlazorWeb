namespace AdminBaker.Entities.Info;

public class ProductoAuditoriaInfo : BaseAuditoriaInfo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public string Relleno { get; set; } = default!;
    public string TipoTorta { get; set; } = default!;
    public double Tamanio { get; set; }
}