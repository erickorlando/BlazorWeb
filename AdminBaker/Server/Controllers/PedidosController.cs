using AdminBaker.Services.Interfaces;
using AdminBaker.Shared.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AdminBaker.Entities;
using AdminBaker.Shared;
using AdminBaker.Shared.Response;

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
    public async Task<IActionResult> Get(DateTime fechaInicio, DateTime fechaFin, string? filter)
    {
        var response = await _service.ListAsync(fechaInicio, fechaFin, filter);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _service.FindByIdAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> ListAudit()
    {
        return Ok(await _service.ListAuditAsync());
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post(PedidoDtoRequest request)
    {
        var email = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;

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

    [Authorize(Roles = Constantes.RolVendedor)]
    [HttpPatch("take/{id:int}")]
    public async Task<IActionResult> Tomar(int id)
    {
        // Recuperamos el ID del vendedor autenticado
        var idVendedor = HttpContext.User.Claims.First(x => x.Type == "IdVendedor").Value;
        if (string.IsNullOrWhiteSpace(idVendedor))
            return BadRequest(new BaseResponse
            {
                ErrorMessage = "No se pudo recuperar el ID del Vendedor"
            });

        var response = await _service.TakeAsync(int.Parse(idVendedor), id);

        return response.Success ? Ok(response) : NotFound(response);
    }

    [Authorize(Roles = Constantes.RolAdministrador)]
    [HttpPatch("{id:int}/cancel")]
    public async Task<IActionResult> Cancelar(int id)
    {
        var response = await _service.CancelAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }

    [Authorize(Roles = $"{Constantes.RolVendedor},{Constantes.RolAdministrador}")]
    [HttpPatch("{id:int}/status/{estado:int}")]
    public async Task<IActionResult> CambiarEstado(int id, int estado)
    {
        var response = await _service.ChangeStateAsync(id, (EstadoPedido)estado);

        return response.Success ? Ok(response) : NotFound(response);
    }
}