namespace AdminBaker.Shared.Response;

public class ClienteDto : CommonDtoResponse
{
    public string Rut { get; set; } = default!;

    public string NombreCompleto { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string? Direccion { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Estado { get; set; } = default!;
    public string? Latitud { get; set; }
    public string? Longitud { get; set; }
}