using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;

namespace AdminBaker.Repositories.Implementations;

public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
{
    public ClienteRepository(AdminBakerDbContext context) : base(context)
    {

    }
}