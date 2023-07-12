namespace AdminBaker.Shared.Response;

public class PaymentOrderDtoResponse
{
    public string OrderId { get; set; } = default!;
    public string ApproveUrl { get; set; } = default!;
}