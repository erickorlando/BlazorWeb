using AdminBaker.Entities;
using AdminBaker.Entities.Info;

namespace AdminBaker.Repositories.Interfaces;

public interface IVendedorRepository : IRepositoryBase<Vendedor>
{
    Task<Vendedor?> FindByEmailAsync(string email);
    Task<ICollection<Vendedor>> ListAsync(string? filter);
    Task<ICollection<VendedorAuditoriaInfo>> ListAuditAsync();
    Task ReactivarAsync(int id);
}