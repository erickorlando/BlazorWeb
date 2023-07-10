using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy;

public interface IPedidoProxy : ICrudRestHelper<PedidoDtoRequest, PedidoDto>
{
    Task<ICollection<PedidoDto>> ListAsync(DateTime fechaInicio, DateTime fechaFin, string? filtro);
}