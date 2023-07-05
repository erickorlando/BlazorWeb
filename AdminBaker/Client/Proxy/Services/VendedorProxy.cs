using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy.Services;

public class VendedorProxy : CrudRestHelperBase<VendedorDtoRequest, VendedorDto>, IVendedorProxy
{
    public VendedorProxy(HttpClient httpClient) :
        base("api/Vendedores", httpClient)
    {
    }
}