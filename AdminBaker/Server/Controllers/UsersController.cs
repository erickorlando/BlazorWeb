using System.Security.Claims;
using AdminBaker.Services.Interfaces;
using AdminBaker.Shared.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminBaker.Server.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    // POST: api/Users/Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginDtoRequest request)
    {
        var response = await _service.LoginAsync(request);

        return response.Success ? Ok(response) : StatusCode(StatusCodes.Status401Unauthorized, response);
    }

    // POST: api/Users/Register
    [HttpPost]
    public async Task<IActionResult> Register(RegistrarUsuarioDto request)
    {
        return Ok(await _service.RegisterAsync(request));
    }

    // POST: api/Users/SendTokenToResetPassword
    [HttpPost]
    public async Task<IActionResult> SendTokenToResetPassword(GenerateTokenToResetDtoRequest request)
    {
        var response = await _service.SendTokenToResetPasswordAsync(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    // POST: api/Users/ResetPassword
    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordDtoRequest request)
    {
        var response = await _service.ResetPasswordAsync(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    // POST: api/Users/ChangePassword
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordDtoRequest request)
    {
        var response = await _service.ChangePasswordAsync(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    // POST: api/Users/UpdateProfile
    [HttpPost]
    public async Task<IActionResult> UpdateProfile(UpdateProfileDtoRequest request)
    {
        var response = await _service.UpdateProfileAsync(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value!;
        var response = await _service.GetProfileAsync(email);
        return response.Success ? Ok(response) : NotFound(response);
    }
}