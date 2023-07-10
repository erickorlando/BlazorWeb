using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminBaker.Repositories.Implementations;

public class VendedorRepository : RepositoryBase<Vendedor>, IVendedorRepository
{
    public VendedorRepository(AdminBakerDbContext context) : base(context)
    {
    }

    public async Task<Vendedor?> FindByEmailAsync(string email)
    {
        return await Context.Set<Vendedor>().FirstOrDefaultAsync(x => x.Email == email);
    }
}