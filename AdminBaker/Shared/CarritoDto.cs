using AdminBaker.Shared.Response;

namespace AdminBaker.Shared;

public class CarritoDto
{
    public ProductoDto ProductoDto { get; set; } = null!;
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal Total { get; set; }
}