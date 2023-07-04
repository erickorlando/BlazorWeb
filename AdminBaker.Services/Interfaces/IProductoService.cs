using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Services.Interfaces;

public interface IProductoService
{

    Task<PaginationResponse<ProductoDto>> ListAsync();

    Task<BaseResponseGeneric<ProductoDto>> FindByIdAsync(int id);

    Task<BaseResponse> CreateAsync(ProductoDtoRequest request);

    Task<BaseResponse> UpdateAsync(int id, ProductoDtoRequest request);

    Task<BaseResponse> DeleteAsync(int id);
}