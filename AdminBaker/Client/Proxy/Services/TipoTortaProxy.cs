using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy.Services;

public class TipoTortaProxy : CrudRestHelperBase<TipoTortaDtoRequest, TipoTortaDto>, ITipoTortaProxy
{
    public TipoTortaProxy(HttpClient httpClient) 
        : base("api/TipoTortas", httpClient)
    {

    }
}