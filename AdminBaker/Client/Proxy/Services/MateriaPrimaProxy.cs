using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy.Services;

public class MateriaPrimaProxy : CrudRestHelperBase<MateriaPrimaDtoRequest, MateriaPrimaDto>, IMateriaPrimaProxy
{
    public MateriaPrimaProxy(HttpClient httpClient) : 
        base("api/MateriaPrimas", httpClient)
    {
    }
}