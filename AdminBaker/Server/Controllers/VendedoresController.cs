using AdminBaker.Services.Interfaces;
using AdminBaker.Shared;
using AdminBaker.Shared.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminBaker.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VendedoresController : ControllerBase
{
    private readonly IVendedorService _service;

    public VendedoresController(IVendedorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string? filter)
    {
        var response = await _service.ListAsync(filter);

        return Ok(response);
    }
    
    [HttpGet("[action]")]
    [Authorize(Roles = Constantes.RolAdministrador)]
    public async Task<IActionResult> ListAudit()
    {
        var response = await _service.ListAuditAsync();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _service.FindByIdAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(VendedorDtoRequest request)
    {
        var response = await _service.CreateAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, VendedorDtoRequest request)
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

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id)
    {
        var response = await _service.ReactivateAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }
}