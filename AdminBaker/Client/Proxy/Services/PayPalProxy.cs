using System.Net.Http.Json;
using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy.Services;

public class PayPalProxy : IPayPalProxy
{
    private readonly HttpClient _httpClient;

    public PayPalProxy(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaymentOrderDtoResponse> CreateOrderAsync(PaymentOrderDtoRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/paypal/createorder", request);
        var result = await response.Content.ReadFromJsonAsync<BaseResponseGeneric<PaymentOrderDtoResponse>>();
        if (result!.Success)
            return result.Data!;
        
        throw new InvalidOperationException(result.ErrorMessage);
    }
}