using AdminBaker.Entities;
using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Services.Interfaces;

public interface IPedidoService
{
    Task<PaginationResponse<PedidoDto>> ListAsync(DateTime fechaInicio, DateTime fechaFin, string? filter);

    Task<BaseResponseGeneric<ICollection<PedidoAuditoriaDto>>> ListAuditAsync();

    Task<BaseResponseGeneric<PedidoDto>> FindByIdAsync(int id);

    Task<BaseResponse> CreateAsync(string email, PedidoDtoRequest request);

    Task<BaseResponse> UpdateAsync(int id, PedidoDtoRequest request);

    Task<BaseResponse> DeleteAsync(int id);

    Task<BaseResponse> TakeAsync(int idVendedor, int id);

    Task<BaseResponse> CancelAsync(int id);

    Task<BaseResponse> ChangeStateAsync(int id, EstadoPedido estado);
}