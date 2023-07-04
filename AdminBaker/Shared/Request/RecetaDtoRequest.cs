using System.ComponentModel.DataAnnotations;

namespace AdminBaker.Shared.Request;

public class RecetaDtoRequest
{
    [StringLength(50)]
    public required string Nombre { get; set; }
    public required string Detalle { get; set; }
}