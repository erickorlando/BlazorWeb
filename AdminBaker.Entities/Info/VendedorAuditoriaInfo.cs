namespace AdminBaker.Entities.Info;

public class VendedorAuditoriaInfo : BaseAuditoriaInfo
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; } = default!;
    public string Rut { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Direccion { get; set; } = default!;
}