namespace AdminBaker.Shared.Request;

public class PedidoDtoRequest
{
    public int? VendedorId { get; set; }
    public int EstadoPedido { get; set; }
    public int TipoPedido { get; set; }
    public decimal TotalVenta { get; set; }
    public string? UrlImagen { get; set; }
    public DateTime? FechaRetiro { get; set; }
}