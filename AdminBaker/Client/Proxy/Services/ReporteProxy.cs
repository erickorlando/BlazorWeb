using System.Net.Http.Json;
using AdminBaker.Shared;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy.Services;

public class ReporteProxy : IReporteProxy
{
    private readonly HttpClient _httpClient;

    public ReporteProxy(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<ICollection<ReporteTipoTortaDto>> GetReporteTipoTortaAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        var response = await _httpClient
            .GetFromJsonAsync<BaseResponseGeneric<ICollection<ReporteTipoTortaDto>>>
                ($"api/reportes/tipotorta/{fechaInicio.ToString(Constantes.FormatoFecha)}/{fechaFin.ToString(Constantes.FormatoFecha)}");
        
        if (response!.Success)
        {
            return response.Data!;
        }

        throw new InvalidOperationException(response.ErrorMessage);
    }

    public async Task<ReporteCantidadDto> GetReporteCantidadesAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        var response = await _httpClient
            .GetFromJsonAsync<BaseResponseGeneric<ReporteCantidadDto>>
                ($"api/reportes/cantidades/{fechaInicio.ToString(Constantes.FormatoFecha)}/{fechaFin.ToString(Constantes.FormatoFecha)}");

        if (response!.Success)
            return response.Data!;
        
        throw new InvalidOperationException(response.ErrorMessage);
    }
}