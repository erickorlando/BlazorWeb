using System.Security.Claims;
using AdminBaker.Services.Interfaces;
using AdminBaker.Shared.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminBaker.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _service;

    public PedidosController(IPedidoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string? filter)
    {
        var response = await _service.ListAsync(filter ?? string.Empty);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _service.FindByIdAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post(PedidoDtoRequest request)
    {
        var email = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email)?.Value!;

        var response = await _service.CreateAsync(email, request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PedidoDtoRequest request)
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