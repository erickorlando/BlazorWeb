using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy;

public interface IVendedorProxy : ICrudRestHelper<VendedorDtoRequest, VendedorDto>
{
    Task<ICollection<VendedorDto>> ListAsync(string? filter);

    Task ReactivateAsync(int id);
}