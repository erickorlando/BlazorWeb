namespace AdminBaker.Shared.Response;

public class AuditoriaDto
{
    public int Id { get; set; }
    public string Estado { get; set; } = default!;
    public string? Usuario { get; set; }
    public DateTime FechaCambio { get; set; }
}