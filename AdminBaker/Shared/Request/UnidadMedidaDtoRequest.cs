using System.ComponentModel.DataAnnotations;

namespace AdminBaker.Shared.Request;

public class UnidadMedidaDtoRequest
{
    [Required]
    public string Codigo { get; set; } = default!;

    [Required]
    public string Descripcion { get; set; } = default!;
}