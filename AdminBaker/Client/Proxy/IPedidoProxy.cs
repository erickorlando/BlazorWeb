using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy;

public interface IPedidoProxy : ICrudRestHelper<PedidoDtoRequest, PedidoDto>
{
    Task<ICollection<PedidoDto>> ListAsync(DateTime fechaInicio, DateTime fechaFin, string? filtro);

    Task TakeAsync(int id);

    Task CancelAsync(int id);

    Task ChangeStatusAsync(int id, int status);
}