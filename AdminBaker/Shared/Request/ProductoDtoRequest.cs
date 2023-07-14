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
    
    //public string TipoTorta { get; set; } = null!;

    [Required]
    public string Relleno { get; set; } = default!;
    public double Tamanio { get; set; }

    public string? ImagenUrl { get; set; }

    public string? Base64Imagen { get; set; }
    public string? FileName { get; set; }
    public bool Especial { get; set; }
    public string? UserName { get; set; }
}