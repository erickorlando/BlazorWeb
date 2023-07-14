using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy;

public interface IProductoProxy : ICrudRestHelper<ProductoDtoRequest, ProductoDto>
{
    Task<ICollection<ProductoDto>> ListTopCarouselAsync();

    Task<ICollection<ProductoDto>> ListAsync(string filter);

    Task<ICollection<ProductoAuditoriaDto>> ListAuditAsync();

}