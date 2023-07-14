namespace AdminBaker.Entities.Info;

public class MateriaPrimaAuditoriaInfo : BaseAuditoriaInfo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public DateTime Caducidad { get; set; }
    public decimal Cantidad { get; set; }
    public string UnidadMedida { get; set; } = default!;
}