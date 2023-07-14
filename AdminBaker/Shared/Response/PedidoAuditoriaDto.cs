namespace AdminBaker.Shared.Response;

public class PedidoAuditoriaDto : AuditoriaDto
{
    public string NroPedido { get; set; } = default!;
    public DateTime Fecha { get; set; }
    public string Cliente { get; set; } = default!;
    public string EstadoPedido { get; set; } = default!;
    public string? Vendedor { get; set; }
}