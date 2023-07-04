using System.ComponentModel.DataAnnotations;

namespace AdminBaker.Entities;

public class Persona : EntityBase
{
    [StringLength(50)]
    public required string Rut { get; set; }

    [StringLength(200)]
    public required string NombreCompleto { get; set; }

    [StringLength(500)]
    public required string Email { get; set; }
}