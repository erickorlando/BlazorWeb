using System.Net.Http.Json;
using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy.Services;

public class ClienteProxy : CrudRestHelperBase<ClienteDtoRequest, ClienteDto>, IClienteProxy
{
    public ClienteProxy(HttpClient httpClient) :
        base("api/Clientes", httpClient)
    {
    }

    public async Task<ICollection<ClienteDto>> ListAsync(string? filter)
    {
        var response = await HttpClient.GetAsync($"{BaseUrl}?filter={filter}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<PaginationResponse<ClienteDto>>();
            if (content!.Success)
            {
                return content.Data!;
            }
        }

        throw new InvalidOperationException(response.ReasonPhrase);
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