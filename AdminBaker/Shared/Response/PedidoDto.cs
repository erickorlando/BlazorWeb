namespace AdminBaker.Shared.Response;

public class PedidoDto : CommonDtoResponse
{
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
    public int EstadoPedido { get; set; } = default!;
    public string? MensajePersonalizado { get; set; }
    public string Origen { get; set; } = default!;

    #region Modal

    public string Modal => $"modal-{Id}";

    public string TargetModal => $"#{Modal}";

    #endregion

    #region Auditoria

    public DateTime? FechaCambio { get; set; }

    #endregion

}