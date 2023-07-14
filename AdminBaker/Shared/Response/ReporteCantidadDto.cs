namespace AdminBaker.Shared.Response;

public class ReporteCantidadDto
{
    public int CantidadProductos { get; set; }
    public int CantidadClientes { get; set; }
    public int CantidadVentas { get; set; }
    public decimal SumaTotalVentas { get; set; }
    public decimal VentaPromedio { get; set; }
}