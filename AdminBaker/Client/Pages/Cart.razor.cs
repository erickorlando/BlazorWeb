using AdminBaker.Client.Auth;
using AdminBaker.Client.Proxy;
using AdminBaker.Shared;
using AdminBaker.Shared.Request;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace AdminBaker.Client.Pages;

public partial class Cart
{
    [Inject] 
    public ICarritoProxy CarritoProxy { get; set; } = default!;

    [Inject] 
    public IPayPalProxy PayPalProxy { get; set; } = default!;

    [Inject] 
    public IJSRuntime JsRuntime { get; set; } = default!;

    [Inject] 
    public ISessionStorageService SessionStorageService { get; set; } = default!;

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

            var request = new PedidoDtoRequest() { Items = listProductos, FechaRetiro = Tarjeta.FechaRetiro };

            var id = await PedidoProxy.CreatePedidoAsync(request);

            var respuesta = await PayPalProxy.CreateOrderAsync(new PaymentOrderDtoRequest
            {
                PedidoId = id
            });

            // despues de abierta la ventana de aprobación, guardamos en la sesion el resultado del Pago
            await SessionStorageService.SaveStorage("paypalResponse", respuesta);

            // abrir en otra ventana la URL de aprobacion de PayPal
            await JsRuntime.InvokeVoidAsync("open", respuesta.ApproveUrl);

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