namespace AdminBaker.Shared.Request;

public class UpdateProfileDtoRequest : PersonaDtoRequest
{
    public DateTime FechaNacimiento { get; set; }
}