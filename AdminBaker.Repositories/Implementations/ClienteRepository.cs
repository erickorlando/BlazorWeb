using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminBaker.Repositories.Implementations;

public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
{
    public ClienteRepository(AdminBakerDbContext context) : base(context)
    {

    }

    public async Task<Cliente?> FindByEmailAsync(string email)
    {
        return await Context.Set<Cliente>().FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<string> GetLastNumberAsync()
    {
        var number = await Context.Set<Pedido>().CountAsync() + 1;
        return $"{number:000000}";
    }
}