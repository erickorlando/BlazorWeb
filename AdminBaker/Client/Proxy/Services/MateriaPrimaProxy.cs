using System.Net.Http.Json;
using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy.Services;

public class MateriaPrimaProxy : CrudRestHelperBase<MateriaPrimaDtoRequest, MateriaPrimaDto>, IMateriaPrimaProxy
{
    public MateriaPrimaProxy(HttpClient httpClient) : 
        base("api/MateriaPrimas", httpClient)
    {
    }

    public async Task<ICollection<MateriaPrimaAuditoriaDto>> ListAuditAsync()
    {
        var response = await HttpClient.GetFromJsonAsync<BaseResponseGeneric<ICollection<MateriaPrimaAuditoriaDto>>>($"{BaseUrl}/ListAudit");
        if (response!.Success)
            return response.Data!;
        
        throw new InvalidOperationException(response.ErrorMessage);
    }
}