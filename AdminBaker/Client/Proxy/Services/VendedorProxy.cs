using System.Net.Http.Json;
using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy.Services;

public class VendedorProxy : CrudRestHelperBase<VendedorDtoRequest, VendedorDto>, IVendedorProxy
{
    public VendedorProxy(HttpClient httpClient) :
        base("api/Vendedores", httpClient)
    {
    }

    public async Task<ICollection<VendedorDto>> ListAsync(string? filter)
    {
        var response = await HttpClient.GetAsync($"{BaseUrl}?filter={filter}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<PaginationResponse<VendedorDto>>();
            if (content!.Success)
            {
                return content.Data!;
            }
        }

        throw new InvalidOperationException(response.ReasonPhrase);
    }

    public async Task<ICollection<VendedorAuditoriaDto>> ListAuditAsync()
    {
        var response = await HttpClient.GetFromJsonAsync<BaseResponseGeneric<ICollection<VendedorAuditoriaDto>>>($"{BaseUrl}/ListAudit");
        if (response!.Success)
            return response.Data!;
        
        throw new InvalidOperationException(response.ErrorMessage);
    }

    public async Task ReactivateAsync(int id)
    {
        var response = await HttpClient.PatchAsync($"{BaseUrl}/{id}", null);
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<BaseResponse>();
            throw new InvalidOperationException(content!.ErrorMessage);
        }
    }
}