namespace AdminBaker.Shared.Request;

public class VendedorDtoRequest : PersonaDtoRequest
{
    public required string CodigoTrabajador { get; set; }
    public required string Horario { get; set; }
}