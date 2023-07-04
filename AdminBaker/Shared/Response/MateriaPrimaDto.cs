namespace AdminBaker.Shared.Response;

public class MateriaPrimaDto : CommonDtoResponse
{
    public required string Nombre { get; set; }
    public decimal Cantidad { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public DateTime Caducidad { get; set; }
}