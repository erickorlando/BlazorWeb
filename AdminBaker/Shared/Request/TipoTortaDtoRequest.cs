using System.ComponentModel.DataAnnotations;

namespace AdminBaker.Shared.Request;

public class TipoTortaDtoRequest
{
    [Required]
    public string Nombre { get; set; } = default!;
}