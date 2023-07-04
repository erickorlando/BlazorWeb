namespace AdminBaker.Shared.Request;

public class MateriaPrimaDtoRequest
{
    public required string Nombre { get; set; }
    public decimal Cantidad { get; set; }
    public int UnidadMedidaId { get; set; }
    public DateTime Caducidad { get; set; }
}