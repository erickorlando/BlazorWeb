using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;

namespace AdminBaker.Repositories.Implementations;

public class TipoTortaRepository : RepositoryBase<TipoTorta>, ITipoTortaRepository
{
    public TipoTortaRepository(AdminBakerDbContext context) : base(context)
    {
    }
}