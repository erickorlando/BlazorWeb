namespace AdminBaker.Shared.Request;

public class UpdateProfileDtoRequest : PersonaDtoRequest
{
    public DateTime FechaNacimiento { get; set; }
    public string? Latitud { get; set; }
    public string? Longitud { get; set; }
}