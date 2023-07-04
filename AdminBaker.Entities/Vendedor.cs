namespace AdminBaker.Entities;

public class Vendedor : Persona
{
    public required string CodigoTrabajador { get; set; }
    public required string Horario { get; set; }
}