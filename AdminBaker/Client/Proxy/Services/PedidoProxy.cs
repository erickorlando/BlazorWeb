using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;
using System.Net.Http.Json;

namespace AdminBaker.Client.Proxy.Services;

public class PedidoProxy : CrudRestHelperBase<PedidoDtoRequest, PedidoDto>, IPedidoProxy
{
    public PedidoProxy(HttpClient httpClient) :
        base("api/Pedidos", httpClient)
    {
    }

    public async Task<ICollection<PedidoDto>> ListAsync(DateTime fechaInicio, DateTime fechaFin, string? filtro)
    {
        var response = await HttpClient.GetAsync($"{BaseUrl}?fechaInicio={fechaInicio:yyyy-MM-dd}&fechaFin={fechaFin:yyyy-MM-dd}&filter={filtro}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<PaginationResponse<PedidoDto>>();
            if (content!.Success)
            {
                return content.Data!;
            }
        }

        throw new InvalidOperationException(response.ReasonPhrase);
    }

    public async Task<ICollection<PedidoAuditoriaDto>> ListAuditAsync()
    {
        var response = await HttpClient.GetFromJsonAsync<BaseResponseGeneric<ICollection<PedidoAuditoriaDto>>>($"{BaseUrl}/ListAudit");
        if (response!.Success)
        {
            return response.Data!;
        }

        throw new InvalidOperationException(response.ErrorMessage);
    }

    public async Task TakeAsync(int id)
    {
        var response = await HttpClient.PatchAsync($"{BaseUrl}/take/{id}", null);

        if (!response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<BaseResponse>();
            throw new InvalidOperationException(data!.ErrorMessage);
        }
    }

    public async Task CancelAsync(int id)
    {
        var response = await HttpClient.PatchAsync($"{BaseUrl}/{id}/cancel", null);

        if (!response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<BaseResponse>();
            throw new InvalidOperationException(data!.ErrorMessage);
        }
    }

    public async Task ChangeStatusAsync(int id, int status)
    {
        var response = await HttpClient.PatchAsync($"{BaseUrl}/{id}/status/{status}", null);

        if (!response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<BaseResponse>();
            throw new InvalidOperationException(data!.ErrorMessage);
        }
    }
}