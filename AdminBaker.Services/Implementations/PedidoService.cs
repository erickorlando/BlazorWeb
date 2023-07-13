using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;
using AdminBaker.Services.Interfaces;
using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AdminBaker.Services.Implementations;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _repository;
    private readonly IClienteRepository _clienteRepository;
    private readonly ILogger<PedidoService> _logger;
    private readonly IMapper _mapper;
    private readonly IFileUploader _fileUploader;
    private readonly IProductoRepository _productoRepository;

    public PedidoService(IPedidoRepository repository,
        IClienteRepository clienteRepository,
        ILogger<PedidoService> logger,
        IMapper mapper,
        IFileUploader fileUploader,
        IProductoRepository productoRepository)
    {
        _repository = repository;
        _clienteRepository = clienteRepository;
        _logger = logger;
        _mapper = mapper;
        _fileUploader = fileUploader;
        _productoRepository = productoRepository;
    }

    public async Task<PaginationResponse<PedidoDto>> ListAsync(DateTime fechaInicio, DateTime fechaFin, string? filter)
    {
        var response = new PaginationResponse<PedidoDto>();

        try
        {
            response.Data = _mapper.Map<ICollection<PedidoDto>>(await _repository.ListAsync(fechaInicio, fechaFin, filter));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Listar";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<PedidoDto>>> ListAuditAsync()
    {
        var response = new BaseResponseGeneric<ICollection<PedidoDto>>();
        try
        {
            response.Data = _mapper.Map<ICollection<PedidoDto>>(await _repository.ListAuditAsync());
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Obtener el registro";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public Task<BaseResponseGeneric<PedidoDto>> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse> CreateAsync(string email, PedidoDtoRequest request)
    {
        var response = new BaseResponse();
        try
        {
            var cliente = await _clienteRepository.FindByEmailAsync(email);
            if (cliente is null)
            {
                response.ErrorMessage = "Solo los clientes pueden crear pedidos";
                response.Success = false;
                return response;
            }

            var pedido = new Pedido
            {
                Fecha = DateTime.Today,
                ClienteId = cliente.Id,
                EstadoPedido = EstadoPedido.Pendiente,
                TipoPedido = (TipoPedido)request.TipoPedido,
            };

            var lastNumber = await _clienteRepository.GetLastNumberAsync();
            pedido.NroPedido = lastNumber;

            var listaItems = new List<PedidoItem>();
            // Si es un pedido especial crear los detalles
            if (pedido.TipoPedido == TipoPedido.PedidoEspecial)
            {
                pedido.UrlImagen = await _fileUploader.UploadFileAsync(request.Base64Imagen, $"especial-{request.FileName}");
                pedido.MensajePersonalizado = request.MensajePersonalizado;
                pedido.FechaRetiro = request.FechaRetiro;

                var productoEspecial = await _productoRepository.GetSpecialAsync();
                if (productoEspecial is null)
                {
                    response.ErrorMessage = "No se encontró el producto especial";
                    response.Success = false;
                    return response;
                }

                var pedidoItem = new PedidoItem
                {
                    Pedido = pedido,
                    ProductoId = productoEspecial.Id,
                    TipoTortaId = request.TipoTortaId,
                    Relleno = request.Relleno ?? productoEspecial.Relleno,
                    Tamanio = request.Tamanio ?? productoEspecial.Tamanio,
                    Cantidad = 1,
                    PrecioUnitario = productoEspecial.Precio,
                    Total = productoEspecial.Precio
                };

                listaItems.Add(pedidoItem);

                await _repository.AddItemAsync(pedidoItem);
            }
            else
            {
                if (request.Items is null || request.Items.Count == 0)
                {
                    response.ErrorMessage = "Debe agregar al menos un item al pedido";
                    response.Success = false;
                    return response;
                }
                foreach (var item in request.Items)
                {
                    var producto = await _productoRepository.FindByIdAsync(item.ProductoId);
                    if (producto is null)
                        continue;
                    
                    var pedidoItem = new PedidoItem
                    {
                        Pedido = pedido,
                        ProductoId = item.ProductoId,
                        TipoTortaId = producto.TipoTortaId,
                        Relleno = producto.Relleno,
                        Tamanio = producto.Tamanio,
                        PrecioUnitario = producto.Precio,
                        Cantidad = item.Cantidad
                    };
                    pedidoItem.Total = pedidoItem.Cantidad * pedidoItem.PrecioUnitario;

                    await _repository.AddItemAsync(pedidoItem);
                    listaItems.Add(pedidoItem);
                }
            }

            pedido.TotalVenta = listaItems.Sum(p => p.Total);

            await _repository.AddAsync(pedido);

            await _repository.UpdateAsync();

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al crear el pedido";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> UpdateAsync(int id, PedidoDtoRequest request)
    {
        var response = new BaseResponse();

        try
        {
            var pedido = await _repository.FindByIdAsync(id);
            if (pedido is null)
            {
                response.ErrorMessage = "Pedido no encontrado";
                response.Success = false;
                return response;
            }

            pedido.EstadoPedido = (EstadoPedido)request.EstadoPedido;

            await _repository.UpdateAsync();

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al actualizar";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> DeleteAsync(int id)
    {
        var response = new BaseResponse();

        try
        {
            await _clienteRepository.DeleteAsync(id);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al eliminar";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> TakeAsync(int idVendedor, int id)
    {
        var response = new BaseResponse();

        try
        {
            await _repository.TomarPedidoAsync(idVendedor, id);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al tomar el pedido";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> CancelAsync(int id)
    {
        var response = new BaseResponse();

        try
        {
            await _repository.CancelarPedidoAsync(id);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al cancelar el pedido";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> ChangeStateAsync(int id, EstadoPedido estado)
    {
        var response = new BaseResponse();

        try
        {
            await _repository.CambiarEstadoAsync(id, estado);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al cambiar el estado del pedido";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }
}