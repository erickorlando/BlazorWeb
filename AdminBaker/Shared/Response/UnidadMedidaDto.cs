namespace AdminBaker.Shared.Response;

public class UnidadMedidaDto : CommonDtoResponse
{
    public string Codigo { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public string Texto => $"{Codigo} - {Descripcion}";
}