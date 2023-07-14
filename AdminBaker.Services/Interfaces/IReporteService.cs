using AdminBaker.Shared.Response;

namespace AdminBaker.Services.Interfaces;

public interface IReporteService
{
    Task<BaseResponseGeneric<ICollection<ReporteTipoTortaDto>>> GetReporteTipoTortaAsync(DateTime fechaInicio,
        DateTime fechaFin);
    
    Task<BaseResponseGeneric<ReporteCantidadDto>> GetReporteCantidadesAsync(DateTime fechaInicio, DateTime fechaFin);
}