namespace AdminBaker.Entities;

public class Cliente : Persona
{
    public DateTime FechaNacimiento { get; set; }
    public string? Latitud { get; set; }
    public string? Longitud { get; set; }
}