namespace AdminBaker.Shared.Request;

public class ResetPasswordDtoRequest
{
    public string Token { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Clave { get; set; } = null!;
    public string ConfirmarClave { get; set; } = null!;
}