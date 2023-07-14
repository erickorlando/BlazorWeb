using System.ComponentModel.DataAnnotations;

namespace AdminBaker.Shared.Request;

public class TarjetaDto
{
    [Required(ErrorMessage = "El titular es requerido")]
    public string? Titular { get; set; }

    [Required(ErrorMessage = "El número de tarjeta es requerido")]
    [MaxLength(16)]
    public string? Numero { get; set; }

    [Required(ErrorMessage = "La vigencia es requerida")]
    [MaxLength(5)]
    public string? Vigencia { get; set; }

    [Required(ErrorMessage = "El CVV es requerido")]
    [MaxLength(3)]
    public string? Cvv { get; set; }

    public DateTime FechaRetiro { get; set; } = DateTime.Today.AddDays(14);
}