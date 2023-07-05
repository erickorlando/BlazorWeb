using AdminBaker.Shared;

namespace AdminBaker.Client.Proxy.Services;

public interface ICarritoServicio
{
    event Action? ActualizarVista;
    Task AgregarCarrito(CarritoDto modelo);
    Task EliminarCarrito(int idProducto);
    int CantidadProductos();
    Task<List<CarritoDto>> ObtenerCarrito();
    Task LimpiarCarrito();
}