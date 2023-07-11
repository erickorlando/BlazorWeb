using System.ComponentModel.DataAnnotations;

namespace AdminBaker.Shared.Request;

public class PersonaDtoRequest
{
    [StringLength(50)]
    [Required]
    public string Rut { get; set; } = default!;

    [StringLength(200)]
    [Required]
    public string NombreCompleto { get; set; } = default!;

    [StringLength(500)]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [StringLength(500)]
    [Required]
    public string? Direccion { get; set; }
}