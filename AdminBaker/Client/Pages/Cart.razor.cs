using AdminBaker.Client.Proxy.Services;
using AdminBaker.Shared.Request;
using AdminBaker.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using AdminBaker.Client.Proxy;

namespace AdminBaker.Client.Pages;

public partial class Cart
{
    [Inject] 
    public ICarritoServicio CarritoProxy { get; set; } = default!;

    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationState { get; set; } = default!;

    private bool _disabled = true;

    public List<CarritoDto> Lista { get; set; } = new List<CarritoDto>();
    public TarjetaDto Tarjeta { get; set; } = new TarjetaDto();

    public bool IsLoading { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Lista = await CarritoProxy.ObtenerCarrito();

        var authState = await AuthenticationState;
        if (authState.User.Identity!.IsAuthenticated)
        {
            _disabled = false;
        }
    }

    private void Disminuir(int id)
    {
        var item = Lista.First(i => i.ProductoDto.Id == id);

        if (item.Cantidad - 1 > 0)
        {
            item.Cantidad--;
            item.Total = item.Cantidad * item.Precio;
        }
    }

    private void Aumentar(int id)
    {
        var item = Lista.First(i => i.ProductoDto.Id == id);
        item.Cantidad++;
        item.Total = item.Cantidad * item.Precio;
    }

    private async Task Eliminar(int idProducto)
    {
        var producto = Lista.First(i => i.ProductoDto.Id == idProducto);
        Lista.Remove(producto);
        await CarritoProxy.EliminarCarrito(idProducto);
    }

    private async Task ProcesarPago()
    {
        try
        {
            if (!Lista.Any())
            {
                ToastService.ShowWarning("No se encontraron productos");
                return;
            }

            IsLoading = true;

            var listProductos = new List<PedidoItemDtoRequest>();

            foreach (var item in Lista)
            {
                listProductos.Add(new PedidoItemDtoRequest(item.ProductoDto.Id, item.Cantidad));
            }

            var request = new PedidoDtoRequest() { Items = listProductos };

            await PedidoProxy.CreateAsync(request);

            await CarritoProxy.LimpiarCarrito();
            ToastService.ShowSuccess("Pedido registrado");
            NavigationManager.NavigateTo("/");
        }
        catch (Exception e)
        {
            ToastService.ShowError(e.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }
}