using AdminBaker.Shared;

namespace AdminBaker.Client.Proxy;

public interface ICarritoProxy
{
    event Action? ActualizarVista;
    Task AgregarCarrito(CarritoDto modelo);
    Task EliminarCarrito(int idProducto);
    int CantidadProductos();
    Task<List<CarritoDto>> ObtenerCarrito();
    Task LimpiarCarrito();
}