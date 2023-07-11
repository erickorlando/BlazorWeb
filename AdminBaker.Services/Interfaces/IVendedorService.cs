using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Services.Interfaces;

public interface IVendedorService
{
    Task<PaginationResponse<VendedorDto>> ListAsync(string? filter);
    Task<BaseResponseGeneric<VendedorDto>> FindByIdAsync(int id);
    Task<BaseResponse> CreateAsync(VendedorDtoRequest request);
    Task<BaseResponse> UpdateAsync(int id, VendedorDtoRequest request);
    Task<BaseResponse> DeleteAsync(int id);
    Task<BaseResponse> ReactivateAsync(int id);
}