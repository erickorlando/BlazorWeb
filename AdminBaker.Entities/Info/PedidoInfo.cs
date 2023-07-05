namespace AdminBaker.Entities.Info;

public class PedidoInfo
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Cliente { get; set; } = null!;
    public int ClienteId { get; set; }
    public string? Vendedor { get; set; }
    public int? VendedorId { get; set; }
    public EstadoPedido EstadoPedido { get; set; }
    public TipoPedido TipoPedido { get; set; }
    public decimal TotalVenta { get; set; }
    public string? UrlImagen { get; set; }
    public DateTime? FechaRetiro { get; set; }
}