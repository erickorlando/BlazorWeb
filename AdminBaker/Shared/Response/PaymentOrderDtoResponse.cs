namespace AdminBaker.Shared.Response;

public class PaymentOrderDtoResponse
{
    public int PedidoId { get; set; }
    public string OrderId { get; set; } = default!;
    public string ApproveUrl { get; set; } = default!;
}