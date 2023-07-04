using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Services.Interfaces;

public interface IUnidadMedidaService
{
    Task<PaginationResponse<UnidadMedidaDto>> ListAsync();

    Task<BaseResponseGeneric<UnidadMedidaDto>> FindByIdAsync(int id);

    Task<BaseResponse> CreateAsync(UnidadMedidaDtoRequest request);

    Task<BaseResponse> UpdateAsync(int id, UnidadMedidaDtoRequest request);

    Task<BaseResponse> DeleteAsync(int id);
}