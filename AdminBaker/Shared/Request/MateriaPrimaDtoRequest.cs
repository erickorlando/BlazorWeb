using System.ComponentModel.DataAnnotations;

namespace AdminBaker.Shared.Request;

public class MateriaPrimaDtoRequest
{
    [Required]
    public string Nombre { get; set; } = default!;
    public decimal Cantidad { get; set; }
    public int UnidadMedidaId { get; set; }
    public DateTime Caducidad { get; set; } = DateTime.Today.AddMonths(6);
}