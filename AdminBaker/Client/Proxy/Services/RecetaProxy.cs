using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy.Services;

public class RecetaProxy : CrudRestHelperBase<RecetaDtoRequest, RecetaDto>, IRecetaProxy
{
    public RecetaProxy(HttpClient httpClient) : 
        base("api/Recetas", httpClient)
    {
    }
}