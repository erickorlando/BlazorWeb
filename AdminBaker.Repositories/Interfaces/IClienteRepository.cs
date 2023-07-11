using AdminBaker.Entities;

namespace AdminBaker.Repositories.Interfaces;

public interface IClienteRepository : IRepositoryBase<Cliente>
{
    Task<Cliente?> FindByEmailAsync(string email);
    Task<string> GetLastNumberAsync();
    Task<ICollection<Cliente>> ListAsync(string? filter);
    Task ReactivarAsync(int id);
}