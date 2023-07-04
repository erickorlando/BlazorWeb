using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;
using AdminBaker.Services.Interfaces;
using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AdminBaker.Services.Implementations;

public class ProductoService : IProductoService
{

    private readonly IProductoRepository _repository;
    private readonly ILogger<ProductoService> _logger;
    private readonly IMapper _mapper;

    public ProductoService(IProductoRepository repository, ILogger<ProductoService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<ProductoDto>> ListAsync()
    {
        var response = new PaginationResponse<ProductoDto>();

        try
        {
            response.Data = _mapper.Map<ICollection<ProductoDto>>(await _repository.ListAsync(p => p.Estado));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Listar";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponseGeneric<ProductoDto>> FindByIdAsync(int id)
    {
        var response = new BaseResponseGeneric<ProductoDto>();
        try
        {
            response.Data = _mapper.Map<ProductoDto>(await _repository.FindByIdAsync(id));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Obtener el registro";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> CreateAsync(ProductoDtoRequest request)
    {
        var response = new BaseResponse();
        try
        {
            await _repository.AddAsync(_mapper.Map<Producto>(request));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Crear";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> UpdateAsync(int id, ProductoDtoRequest request)
    {
        var response = new BaseResponse();
        try
        {
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null)
            {
                response.ErrorMessage = "Registro no encontrado";
                return response;
            }

            _mapper.Map(request, entity);
            await _repository.UpdateAsync();
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Actualizar";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> DeleteAsync(int id)
    {
        var response = new BaseResponse();
        try
        {
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null)
            {
                response.ErrorMessage = "Registro no encontrado";
                return response;
            }

            await _repository.DeleteAsync(id);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Eliminar";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }   
}