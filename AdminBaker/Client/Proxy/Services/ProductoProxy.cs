using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy.Services;

public class ProductoProxy : CrudRestHelperBase<ProductoDtoRequest, ProductoDto>, IProductoProxy
{
    public ProductoProxy(HttpClient httpClient) :
        base("api/Productos", httpClient)
    {
    }
}