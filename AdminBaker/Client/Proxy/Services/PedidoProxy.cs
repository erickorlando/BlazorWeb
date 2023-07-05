using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy.Services;

public class PedidoProxy : CrudRestHelperBase<PedidoDtoRequest, PedidoDto>, IPedidoProxy
{
    public PedidoProxy(HttpClient httpClient) :
        base("api/Pedidos", httpClient)
    {
    }
}