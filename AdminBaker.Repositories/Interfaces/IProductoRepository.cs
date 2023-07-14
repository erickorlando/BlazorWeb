using AdminBaker.Entities;
using AdminBaker.Entities.Info;

namespace AdminBaker.Repositories.Interfaces;

public interface IProductoRepository : IRepositoryBase<Producto>
{
    Task<ICollection<ProductoInfo>> ListAsync(string? filtro);

    Task<ICollection<ProductoInfo>> ListTopCarousel();

    Task<Producto?> GetSpecialAsync();

    Task<ICollection<ProductoAuditoriaInfo>> ListAuditAsync();

}