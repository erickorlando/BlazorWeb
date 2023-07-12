using System.ComponentModel.DataAnnotations;

namespace AdminBaker.Shared.Request;

public class ChangePasswordDtoRequest
{
    [Required]
    public string OldPassword { get; set; } = default!;
    
    [Compare(nameof(ConfirmNewPassword))]
    public string NewPassword { get; set; } = default!;

    [Required]
    public string ConfirmNewPassword { get; set; } = default!;

    public string Email { get; set; } = string.Empty;
}