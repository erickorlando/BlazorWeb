using System.ComponentModel.DataAnnotations;

namespace AdminBaker.Shared.Request;

public class RegistrarUsuarioDto
{
    [Required]
    [StringLength(20)]
    [Display(Name = "RUT")]
    public string Rut { get; set; } = null!;

    [Required]
    [StringLength(200)]
    [Display(Name = "Nombres")]
    public string NombreCompleto { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;

    [StringLength(20)]
    public string Telefono { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Latitud { get; set; }

    public string? Longitud { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public string Clave { get; set; } = null!;
    
    [Compare(nameof(Clave))]
    public string ConfirmarClave { get; set; } = null!;

    public bool Vendedor { get; set; }

    public RegistrarUsuarioDto()
    {
        FechaNacimiento = DateTime.Today.AddDays(-18);
    }
}