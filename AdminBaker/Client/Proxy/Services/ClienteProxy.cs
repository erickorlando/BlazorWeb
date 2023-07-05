using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy.Services;

public class ClienteProxy : CrudRestHelperBase<ClienteDtoRequest, ClienteDto>, IClienteProxy
{
    public ClienteProxy(HttpClient httpClient) :
        base("api/Clientes", httpClient)
    {
    }
}