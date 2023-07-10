namespace AdminBaker.Shared.Request;

public class PedidoDtoRequest
{
    public int? VendedorId { get; set; }
    public int EstadoPedido { get; set; }
    public int TipoPedido { get; set; }
    public decimal TotalVenta { get; set; }
    public string? UrlImagen { get; set; }
    public DateTime? FechaRetiro { get; set; }
    public string? Base64Imagen { get; set; }
    public string? FileName { get; set; }
    public string? ImagenUrl { get; set; }

    public string? Relleno { get; set; } 
    public double? Tamanio { get; set; }
    public int TipoTortaId { get; set; }
    public string? MensajePersonalizado { get; set; }

    public ICollection<PedidoItemDtoRequest>? Items { get; set; } 
}

public class PedidoItemDtoRequest
{
    public PedidoItemDtoRequest()
    {
        
    }

    public PedidoItemDtoRequest(int productoId, decimal cantidad)
    {
        ProductoId = productoId;
        Cantidad = cantidad;
    }

    public int ProductoId { get; set; }
    public int TipoTortaId { get; set; }
    public decimal Cantidad { get; set; }
}