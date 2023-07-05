using System.ComponentModel.DataAnnotations;

namespace AdminBaker.Shared.Request;

public class RecetaDtoRequest
{
    [StringLength(50)] 
    public string Nombre { get; set; } = default!;
    public string Detalle { get; set; } = default!;
}