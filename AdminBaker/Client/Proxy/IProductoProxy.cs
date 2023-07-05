using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy;

public interface IProductoProxy : ICrudRestHelper<ProductoDtoRequest, ProductoDto>
{
    public Task<ICollection<ProductoDto>> ListTopCarouselAsync();

}