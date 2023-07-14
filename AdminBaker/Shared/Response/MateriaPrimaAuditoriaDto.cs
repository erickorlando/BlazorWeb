namespace AdminBaker.Shared.Response;

public class MateriaPrimaAuditoriaDto : AuditoriaDto
{
    public string Nombre { get; set; } = default!;
    public DateTime Caducidad { get; set; }
    public decimal Cantidad { get; set; }
    public string UnidadMedida { get; set; } = default!;
}