using System.ComponentModel.DataAnnotations;

namespace AdminBaker.Shared.Request;

public class ProductoDtoRequest
{
    [Required]
    public string Nombre { get; set; } = string.Empty;
    
    [Range(0, 9999)]
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public int TipoTortaId { get; set; }
    
    [Required]
    public string TipoTorta { get; set; } = default!;

    [Required]
    public string Relleno { get; set; } = default!;
    public double Tamanio { get; set; }
}