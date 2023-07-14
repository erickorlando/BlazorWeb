using System.Security.Claims;
using AdminBaker.Services;
using AdminBaker.Services.Interfaces;
using AdminBaker.Shared;
using AdminBaker.Shared.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminBaker.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _service;

    public ProductosController(IProductoService service)
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
    
    [HttpGet("carousel")]
    public async Task<IActionResult> GetCarousel()
    {
        var response = await _service.ListTopCarousel();

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
    public async Task<IActionResult> Post(ProductoDtoRequest request)
    {
        request.UserName = Utils.ParseUserName(HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Email).Value);
        var response = await _service.CreateAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, ProductoDtoRequest request)
    {
        request.UserName = Utils.ParseUserName(HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Email).Value);
        var response = await _service.UpdateAsync(id, request);

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _service.DeleteAsync(id, Utils.ParseUserName(HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Email).Value));

        return response.Success ? Ok(response) : NotFound(response);
    }
}