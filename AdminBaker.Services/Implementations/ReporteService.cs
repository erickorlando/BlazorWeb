using AdminBaker.Repositories.Interfaces;
using AdminBaker.Services.Interfaces;
using AdminBaker.Shared.Response;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AdminBaker.Services.Implementations;

public class ReporteService : IReporteService
{
    private readonly IPedidoRepository _repository;
    private readonly ILogger<ReporteService> _logger;
    private readonly IMapper _mapper;

    public ReporteService(IPedidoRepository repository, ILogger<ReporteService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<BaseResponseGeneric<ICollection<ReporteTipoTortaDto>>> GetReporteTipoTortaAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        var response = new BaseResponseGeneric<ICollection<ReporteTipoTortaDto>>();

        try
        {

            response.Data =
                _mapper.Map<ICollection<ReporteTipoTortaDto>>(
                    await _repository.GetReporteTipoTortaTotalAsync(fechaInicio, fechaFin));

            response.Success = true;

        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al cargar el reporte";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponseGeneric<ReporteCantidadDto>> GetReporteCantidadesAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        var response = new BaseResponseGeneric<ReporteCantidadDto>();

        try
        {

            response.Data =
                _mapper.Map<ReporteCantidadDto>(
                    await _repository.GetReporteCantidadesAsync(fechaInicio, fechaFin));

            response.Success = true;

        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al cargar el reporte";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }
}