using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Services.Interfaces;

public interface IPedidoService
{
    Task<PaginationResponse<PedidoDto>> ListAsync(string filter);

    Task<BaseResponseGeneric<PedidoDto>> FindByIdAsync(int id);

    Task<BaseResponse> CreateAsync(string email, PedidoDtoRequest request);

    Task<BaseResponse> UpdateAsync(int id, PedidoDtoRequest request);

    Task<BaseResponse> DeleteAsync(int id);
}