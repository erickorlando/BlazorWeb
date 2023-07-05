using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;
using System.Net.Http.Json;

namespace AdminBaker.Client.Proxy.Services;

public class ProductoProxy : CrudRestHelperBase<ProductoDtoRequest, ProductoDto>, IProductoProxy
{
    public ProductoProxy(HttpClient httpClient) :
        base("api/Productos", httpClient)
    {
    }

    public async Task<ICollection<ProductoDto>> ListTopCarouselAsync()
    {
        var response = await HttpClient.GetFromJsonAsync<BaseResponseGeneric<ICollection<ProductoDto>>>($"{BaseUrl}/carousel");
        if (response!.Success)
            return response.Data!;

        throw new InvalidOperationException(response.ErrorMessage);
    }
}