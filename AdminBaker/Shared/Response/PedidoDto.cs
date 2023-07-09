namespace AdminBaker.Shared.Response;

public class PedidoDto : CommonDtoResponse
{
    public DateTime Fecha { get; set; }
    public string Cliente { get; set; } = null!;
    public int ClienteId { get; set; }
    public string? Vendedor { get; set; }
    public int? VendedorId { get; set; }
    public int EstadoPedido { get; set; }
    public int TipoPedido { get; set; }
    public decimal TotalVenta { get; set; }
    public string? UrlImagen { get; set; }
    public DateTime? FechaRetiro { get; set; }
}