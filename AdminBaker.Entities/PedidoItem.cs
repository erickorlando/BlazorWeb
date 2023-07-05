namespace AdminBaker.Entities;

public class PedidoItem : EntityBase
{
    public required Pedido Pedido { get; set; }
    public int PedidoId { get; set; }
    public Producto Producto { get; set; } = default!;
    public int ProductoId { get; set; }
    public TipoTorta TipoTorta { get; set; } = default!;
    public int TipoTortaId { get; set; }
    public double Tamanio { get; set; }
    public required string Relleno { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Cantidad { get; set; }
    public decimal Total { get; set; }
}