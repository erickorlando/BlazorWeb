namespace AdminBaker.Shared.Request;

public class ChangePasswordDtoRequest
{
    public string OldPassword { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
    public string ConfirmNewPassword { get; set; } = default!;
    public string Email { get; set; } = string.Empty;
}