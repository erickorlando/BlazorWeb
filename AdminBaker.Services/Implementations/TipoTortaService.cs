using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;
using AdminBaker.Services.Interfaces;
using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AdminBaker.Services.Implementations;

public class TipoTortaService : ITipoTortaService
{
    private readonly ITipoTortaRepository _repository;
    private readonly ILogger<TipoTortaService> _logger;
    private readonly IMapper _mapper;

    public TipoTortaService(ITipoTortaRepository repository, ILogger<TipoTortaService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<TipoTortaDto>> ListAsync()
    {
        var response = new PaginationResponse<TipoTortaDto>();

        try
        {
            response.Data = _mapper.Map<ICollection<TipoTortaDto>>(await _repository.ListAsync(p => p.Estado));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Listar";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponseGeneric<TipoTortaDto>> FindByIdAsync(int id)
    {
        var response = new BaseResponseGeneric<TipoTortaDto>();
        try
        {
            response.Data = _mapper.Map<TipoTortaDto>(await _repository.FindByIdAsync(id));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Obtener el registro";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> CreateAsync(TipoTortaDtoRequest request)
    {
        var response = new BaseResponse();
        try
        {
            await _repository.AddAsync(_mapper.Map<TipoTorta>(request));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al Crear";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> UpdateAsync(int id, TipoTortaDtoRequest request)
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