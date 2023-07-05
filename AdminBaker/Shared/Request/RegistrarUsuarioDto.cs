using System.ComponentModel.DataAnnotations;

namespace AdminBaker.Shared.Request;

public class RegistrarUsuarioDto
{
    [Required]
    [StringLength(20)] 
    public string Rut { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string NombreCompleto { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;

    [StringLength(20)]
    public string Telefono { get; set; } = null!;

    public string? Direccion { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public string Clave { get; set; } = null!;
    
    [Compare(nameof(Clave))]
    public string ConfirmarClave { get; set; } = null!;
}