using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Services.Interfaces;

public interface ITipoTortaService
{
    Task<PaginationResponse<TipoTortaDto>> ListAsync();

    Task<BaseResponseGeneric<TipoTortaDto>> FindByIdAsync(int id);

    Task<BaseResponse> CreateAsync(TipoTortaDtoRequest request);

    Task<BaseResponse> UpdateAsync(int id, TipoTortaDtoRequest request);

    Task<BaseResponse> DeleteAsync(int id);
}