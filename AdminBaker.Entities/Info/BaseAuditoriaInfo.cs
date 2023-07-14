namespace AdminBaker.Entities.Info;

public class BaseAuditoriaInfo
{
    public string? Usuario { get; set; } 
    public string Estado { get; set; } = default!;
    public DateTime FechaCambio { get; set; }
}