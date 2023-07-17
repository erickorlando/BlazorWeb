using System.ComponentModel.DataAnnotations;

namespace AdminBaker.Shared.Request;

public class TarjetaDto
{
    public DateTime FechaRetiro { get; set; } = DateTime.Today.AddDays(14);
}