namespace AdminBaker.Shared.Response;

public class VendedorDto : CommonDtoResponse
{
    public string CodigoTrabajador { get; set; } = default!;
    
    public string Horario { get; set; } = default!;

    public string Rut { get; set; } = default!;

    public string NombreCompleto { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string? Direccion { get; set; }
    
    public string Estado { get; set; } = default!;
}