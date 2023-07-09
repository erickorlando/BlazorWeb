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

    public async Task<PaginationResponse<PedidoDto>> ListAsync(string filter)
    {
        var response = new PaginationResponse<PedidoDto>();

        try
        {
            response.Data = _mapper.Map<ICollection<PedidoDto>>(await _repository.ListAsync(filter));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Listar";
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
                TipoPedido = (TipoPedido)request.TipoPedido
            };

            var listaItems = new List<PedidoItem>();
            // Si es un pedido especial crear los detalles
            if (pedido.TipoPedido == TipoPedido.PedidoEspecial)
            {
                pedido.UrlImagen = await _fileUploader.UploadFileAsync(request.Base64Imagen, $"especial-{request.FileName}");

                var pedidoItem = new PedidoItem
                {
                    Pedido = pedido,
                    ProductoId = 1,
                    TipoTortaId = request.TipoTortaId,
                    Relleno = request.Relleno ?? string.Empty,
                    Tamanio = request.Tamanio ?? 0,
                    Cantidad = 1,
                    PrecioUnitario = 10,
                    Total = 10
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

    public Task<BaseResponse> UpdateAsync(int id, PedidoDtoRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}