using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy;

public interface IPayPalProxy
{
    Task<PaymentOrderDtoResponse> CreateOrderAsync(PaymentOrderDtoRequest request);
}