using AdminBaker.Entities;
using AdminBaker.Entities.Info;

namespace AdminBaker.Repositories.Interfaces;

public interface IClienteRepository : IRepositoryBase<Cliente>
{
    Task<Cliente?> FindByEmailAsync(string email);
    Task<string> GetLastNumberAsync();
    Task<ICollection<Cliente>> ListAsync(string? filter);
    Task<ICollection<ClienteAuditoriaInfo>> ListAuditAsync();
    Task ReactivarAsync(int id);
}