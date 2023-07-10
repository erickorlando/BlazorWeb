using System.Net.Http.Json;
using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

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
}