using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy;

public interface IReporteProxy
{
    Task<ICollection<ReporteTipoTortaDto>> GetReporteTipoTortaAsync(DateTime fechaInicio, DateTime fechaFin);
    
    Task<ReporteCantidadDto> GetReporteCantidadesAsync(DateTime fechaInicio, DateTime fechaFin);
}