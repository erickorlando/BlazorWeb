using AdminBaker.Services.Interfaces;
using AdminBaker.Shared.Request;
using Microsoft.AspNetCore.Mvc;

namespace AdminBaker.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MateriaPrimasController : ControllerBase
{
    private readonly IMateriaPrimaService _service;

    public MateriaPrimasController(IMateriaPrimaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string? filter)
    {
        var response = await _service.ListAsync();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _service.FindByIdAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(MateriaPrimaDtoRequest request)
    {
        var response = await _service.CreateAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, MateriaPrimaDtoRequest request)
    {
        var response = await _service.UpdateAsync(id, request);

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _service.DeleteAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }
}