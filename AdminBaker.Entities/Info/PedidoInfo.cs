namespace AdminBaker.Entities.Info;

public class PedidoInfo
{
    public int Id { get; set; }
    public string? NroPedido { get; set; }
    public string? UrlImagen { get; set; }
    public DateTime Fecha { get; set; }
    public float Tamanio { get; set; } 
    public string Relleno { get; set; } = default!;
    public string TipoTorta { get; set; } = default!;
    public decimal Precio { get; set; }
    public decimal Cantidad { get; set; }
    public int ClienteId { get; set; }
    public string Cliente { get; set; } = null!;
    public string Direccion { get; set; } = default!;
    public string Rut { get; set; } = default!;
    public string? Vendedor { get; set; }
    public int? VendedorId { get; set; }
    public decimal TotalVenta { get; set; }
    public DateTime? FechaRetiro { set; get; }
    public EstadoPedido EstadoPedido { get; set; }
    public string? MensajePersonalizado { get; set; }
    public string Origen { get; set; } = default!;
}