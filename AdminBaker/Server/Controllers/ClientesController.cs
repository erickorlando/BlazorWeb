using AdminBaker.Services.Interfaces;
using AdminBaker.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminBaker.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClientesController(IClienteService service)
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

        [HttpDelete("{id}")]
        [Authorize(Roles = Constantes.RolAdministrador)]
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
}
