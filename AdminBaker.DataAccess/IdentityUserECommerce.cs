using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AdminBaker.DataAccess;

public class IdentityUserECommerce : IdentityUser
{
    [StringLength(50)] 
    public string Rut { get; set; } = default!;

    [StringLength(200)]
    public string NombreCompleto { get; set; } = default!;

    public DateTime FechaNacimiento { get; set; }

    [StringLength(500)]
    public string? Direccion { get; set; }
}