using AdminBaker.Entities;

namespace AdminBaker.Repositories.Interfaces;

public interface IVendedorRepository : IRepositoryBase<Vendedor>
{
    Task<Vendedor?> FindByEmailAsync(string email);
    Task<ICollection<Vendedor>> ListAsync(string? filter);
    Task ReactivarAsync(int id);
}