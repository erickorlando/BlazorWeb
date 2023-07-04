using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Services.Interfaces;

public interface IRecetaService
{

    Task<PaginationResponse<RecetaDto>> ListAsync();

    Task<BaseResponseGeneric<RecetaDto>> FindByIdAsync(int id);

    Task<BaseResponse> CreateAsync(RecetaDtoRequest request);

    Task<BaseResponse> UpdateAsync(int id, RecetaDtoRequest request);

    Task<BaseResponse> DeleteAsync(int id);
}