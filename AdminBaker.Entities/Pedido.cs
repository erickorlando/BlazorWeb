namespace AdminBaker.Entities;

public class Pedido : EntityBase
{
    public DateTime Fecha { get; set; }
    public required Cliente Cliente { get; set; }
    public int ClienteId { get; set; }
    public Vendedor? Vendedor { get; set; }
    public int? VendedorId { get; set; }
    public EstadoPedido EstadoPedido { get; set; }
    public TipoPedido TipoPedido { get; set; }
    public decimal TotalVenta { get; set; }
    public string? UrlImagen { get; set; }
    public DateTime? FechaRetiro { get; set; }
}