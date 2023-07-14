using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Services.Interfaces;

public interface IMateriaPrimaService
{

    Task<PaginationResponse<MateriaPrimaDto>> ListAsync();
    
    Task<BaseResponseGeneric<ICollection<MateriaPrimaAuditoriaDto>>> ListAuditAsync();

    Task<BaseResponseGeneric<MateriaPrimaDto>> FindByIdAsync(int id);

    Task<BaseResponse> CreateAsync(MateriaPrimaDtoRequest request);

    Task<BaseResponse> UpdateAsync(int id, MateriaPrimaDtoRequest request);

    Task<BaseResponse> DeleteAsync(int id, string usuario);
}