using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy;

public interface IClienteProxy : ICrudRestHelper<ClienteDtoRequest, ClienteDto>
{
    Task<ICollection<ClienteDto>> ListAsync(string? filter);

    Task ReactivateAsync(int id);
}