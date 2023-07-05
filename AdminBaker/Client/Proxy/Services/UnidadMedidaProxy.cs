using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy.Services;

public class UnidadMedidaProxy : CrudRestHelperBase<UnidadMedidaDtoRequest, UnidadMedidaDto>, IUnidadMedidaProxy
{
    public UnidadMedidaProxy(HttpClient httpClient) 
        : base("api/UnidadMedida", httpClient)
    {
    }
}