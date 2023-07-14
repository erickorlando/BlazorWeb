﻿namespace AdminBaker.Shared.Response;

public class ClienteAuditoriaDto : AuditoriaDto
{
    public string NombreCompleto { get; set; } = default!;
    public string Rut { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Direccion { get; set; } = default!;
    public DateTime FechaNacimiento { get; set; } 
}