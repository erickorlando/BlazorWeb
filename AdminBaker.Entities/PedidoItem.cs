namespace AdminBaker.Entities;

public class PedidoItem : EntityBase
{
    public required Pedido Pedido { get; set; }
    public int PedidoId { get; set; }
    public required Producto Producto { get; set; }
    public int ProductoId { get; set; }
    public required TipoTorta TipoTorta { get; set; }
    public int TipoTortaId { get; set; }
    public double Tamanio { get; set; }
    public required string Relleno { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Cantidad { get; set; }
}