using AdminBaker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminBaker.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly IReporteService _service;

    public ReportesController(IReporteService service)
    {
        _service = service;
    }
    
    [HttpGet("tipotorta/{fechaInicio:datetime}/{fechaFin:datetime}")]
    public async Task<IActionResult> GetReporteTipoTortaAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        var response = await _service.GetReporteTipoTortaAsync(fechaInicio, fechaFin);
        return response.Success ? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("cantidades/{fechaInicio:datetime}/{fechaFin:datetime}")]
    public async Task<IActionResult> GetReporteCantidadesAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        var response = await _service.GetReporteCantidadesAsync(fechaInicio, fechaFin);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}