using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;

namespace AdminBaker.Repositories.Implementations;

public class RecetaRepository : RepositoryBase<Receta>, IRecetaRepository
{
    public RecetaRepository(AdminBakerDbContext context) : base(context)
    {
    }
}