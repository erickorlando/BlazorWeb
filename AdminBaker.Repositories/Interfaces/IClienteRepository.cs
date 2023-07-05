using AdminBaker.Entities;

namespace AdminBaker.Repositories.Interfaces;

public interface IClienteRepository : IRepositoryBase<Cliente>
{
    Task<Cliente?> FindByEmailAsync(string email);
}