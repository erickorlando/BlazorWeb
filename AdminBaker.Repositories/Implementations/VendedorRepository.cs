using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;

namespace AdminBaker.Repositories.Implementations;

public class VendedorRepository : RepositoryBase<Vendedor>, IVendedorRepository
{
    public VendedorRepository(AdminBakerDbContext context) : base(context)
    {
    }
}