namespace AdminBaker.Entities;

public class MateriaPrima : EntityBase
{
    public required string Nombre { get; set; }
    public decimal Cantidad { get; set; }
    public required UnidadMedida UnidadMedida { get; set; }
    public int UnidadMedidaId { get; set; }
    public DateTime Caducidad { get; set; }
}