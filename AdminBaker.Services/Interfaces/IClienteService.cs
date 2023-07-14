using AdminBaker.Shared.Response;

namespace AdminBaker.Services.Interfaces;

public interface IClienteService
{
    Task<PaginationResponse<ClienteDto>> ListAsync(string? filter);

    Task<BaseResponseGeneric<ICollection<ClienteAuditoriaDto>>> ListAuditAsync();

    Task<BaseResponse> DeleteAsync(int id);

    Task<BaseResponse> ReactivateAsync(int id);
}