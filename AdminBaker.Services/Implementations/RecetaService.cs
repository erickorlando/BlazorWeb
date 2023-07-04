using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;
using AdminBaker.Services.Interfaces;
using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AdminBaker.Services.Implementations;

public class RecetaService : IRecetaService
{
    private readonly IRecetaRepository _repository;
    private readonly ILogger<RecetaService> _logger;
    private readonly IMapper _mapper;

    public RecetaService(IRecetaRepository repository, ILogger<RecetaService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<RecetaDto>> ListAsync()
    {
        var response = new PaginationResponse<RecetaDto>();

        try
        {
            response.Data = _mapper.Map<ICollection<RecetaDto>>(await _repository.ListAsync(p => p.Estado));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Listar";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponseGeneric<RecetaDto>> FindByIdAsync(int id)
    {
        var response = new BaseResponseGeneric<RecetaDto>();
        try
        {
            response.Data = _mapper.Map<RecetaDto>(await _repository.FindByIdAsync(id));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Obtener el registro";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> CreateAsync(RecetaDtoRequest request)
    {
        var response = new BaseResponse();
        try
        {
            await _repository.AddAsync(_mapper.Map<Receta>(request));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Crear";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> UpdateAsync(int id, RecetaDtoRequest request)
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