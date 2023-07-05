using System.ComponentModel.DataAnnotations;

namespace AdminBaker.Shared.Request;

public class LoginDtoRequest
{
    [Required] public string UserName { get; set; } = null!;

    [Required] public string Password { get; set; } = null!;
}