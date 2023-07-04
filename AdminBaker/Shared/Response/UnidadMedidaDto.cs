namespace AdminBaker.Shared.Response;

public class UnidadMedidaDto
{
    public string Nombre { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public string Texto => $"{Nombre} - {Descripcion}";
}