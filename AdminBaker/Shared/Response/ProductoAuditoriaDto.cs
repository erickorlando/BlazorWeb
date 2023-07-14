namespace AdminBaker.Shared.Response;

public class ProductoAuditoriaDto : AuditoriaDto
{
    public string Nombre { get; set; } = default!;
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public string Relleno { get; set; } = default!;
    public string TipoTorta { get; set; } = default!;
    public double Tamanio { get; set; }
}