namespace AdminBaker.Shared.Response;

public class RecetaDto : CommonDtoResponse
{
    public string Nombre { get; set; } = default!;
    public string Detalle { get; set; } = default!;
}