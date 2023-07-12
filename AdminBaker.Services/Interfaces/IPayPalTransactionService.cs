using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Services.Interfaces;

public interface IPayPalTransactionService
{
    Task<BaseResponseGeneric<PaymentOrderDtoResponse>> CreateOrderAsync(PaymentOrderDtoRequest request);

    Task<BaseResponse> CapturePaymentAsync(string orderId);
}