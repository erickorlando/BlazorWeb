namespace AdminBaker.Shared.Request;

public class GenerateTokenToResetDtoRequest
{
    public string Email { get; set; } = default!;
}