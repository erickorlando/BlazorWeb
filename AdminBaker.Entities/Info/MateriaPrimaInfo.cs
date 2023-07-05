namespace AdminBaker.Entities.Info;

public class MateriaPrimaInfo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public decimal Cantidad { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public int UnidadMedidaId { get; set; }
    public DateTime Caducidad { get; set; }
}