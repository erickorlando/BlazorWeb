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
    private readonly IFileUploader _fileUploader;

    public ProductoService(IProductoRepository repository, ILogger<ProductoService> logger, IMapper mapper, IFileUploader fileUploader)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _fileUploader = fileUploader;
    }

    public async Task<PaginationResponse<ProductoDto>> ListAsync(string? filter)
    {
        var response = new PaginationResponse<ProductoDto>();

        try
        {
            response.Data = _mapper.Map<ICollection<ProductoDto>>(await _repository.ListAsync(filter ?? string.Empty));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Listar";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<ProductoAuditoriaDto>>> ListAuditAsync()
    {
        var response = new BaseResponseGeneric<ICollection<ProductoAuditoriaDto>>();
        try
        {
            response.Data = _mapper.Map<ICollection<ProductoAuditoriaDto>>(await _repository.ListAuditAsync());
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Lista la auditoria";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<ProductoDto>>> ListTopCarousel()
    {
        var response = new BaseResponseGeneric<ICollection<ProductoDto>>();

        try
        {
            response.Data = _mapper.Map<ICollection<ProductoDto>>(await _repository.ListTopCarousel());
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Cargar el top de productos";
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
            var producto = _mapper.Map<Producto>(request);

            producto.ImagenUrl = await _fileUploader.UploadFileAsync(request.Base64Imagen, request.FileName);
            producto.Usuario = request.UserName;

            await _repository.AddAsync(producto);
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

            if (!string.IsNullOrWhiteSpace(request.FileName))
            {
                entity.ImagenUrl = await _fileUploader.UploadFileAsync(request.Base64Imagen, request.FileName);
                request.ImagenUrl = entity.ImagenUrl;
            }

            _mapper.Map(request, entity);
            entity.Usuario = request.UserName;
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

    public async Task<BaseResponse> DeleteAsync(int id, string userName)
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

            entity.Usuario = userName;
            entity.Estado = false;
            await _repository.UpdateAsync();
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