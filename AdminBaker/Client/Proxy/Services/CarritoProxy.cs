using AdminBaker.Shared;
using Blazored.LocalStorage;
using Blazored.Toast.Services;

namespace AdminBaker.Client.Proxy.Services;

public class CarritoProxy : ICarritoProxy
{
    private readonly ILocalStorageService _localStorageService;
    private readonly ISyncLocalStorageService _syncLocalStorage;
    private readonly IToastService _toastService;

    public CarritoProxy(ILocalStorageService localStorageService,
        ISyncLocalStorageService syncLocalStorage,
        IToastService toastService)
    {
        _localStorageService = localStorageService;
        _syncLocalStorage = syncLocalStorage;
        _toastService = toastService;
    }

    public event Action? ActualizarVista;

    public async Task AgregarCarrito(CarritoDto modelo)
    {
        try
        {
            var carrito = await _localStorageService.GetItemAsync<List<CarritoDto>>("carrito")
                          ?? new List<CarritoDto>();

            var producto = carrito.FirstOrDefault(x => x.ProductoDto.Id == modelo.ProductoDto.Id);
            if (producto is not null)
                carrito.Remove(producto);

            carrito.Add(modelo);
            await _localStorageService.SetItemAsync("carrito", carrito);

            _toastService.ShowSuccess(producto is not null
                ? "Producto fue actualizado en el carrito"
                : "Producto fue agregado al carrito");

            ActualizarVista?.Invoke();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _toastService.ShowError("No se puede agregar al carrito");
        }
    }

    public async Task EliminarCarrito(int idProducto)
    {
        try
        {
            var carrito = await _localStorageService.GetItemAsync<List<CarritoDto>>("carrito");
            if (carrito is not null)
            {
                var elemento = carrito.FirstOrDefault(p => p.ProductoDto.Id == idProducto);
                if (elemento is not null)
                {
                    carrito.Remove(elemento);
                    await _localStorageService.SetItemAsync("carrito", carrito);
                    ActualizarVista?.Invoke();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _toastService.ShowError("No se pudo quitar del carrito");
        }
    }

    public int CantidadProductos()
    {
        var carrito = _syncLocalStorage.GetItem<List<CarritoDto>>("carrito");
        return carrito?.Count ?? 0;
    }

    public async Task<List<CarritoDto>> ObtenerCarrito()
    {
        return await _localStorageService.GetItemAsync<List<CarritoDto>>("carrito") ?? new List<CarritoDto>();
    }

    public async Task LimpiarCarrito()
    {
        await _localStorageService.RemoveItemAsync("carrito");
        ActualizarVista?.Invoke();
    }
}