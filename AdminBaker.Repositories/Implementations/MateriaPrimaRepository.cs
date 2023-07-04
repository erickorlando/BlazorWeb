using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;

namespace AdminBaker.Repositories.Implementations;

public class MateriaPrimaRepository : RepositoryBase<MateriaPrima>, IMateriaPrimaRepository
{
    public MateriaPrimaRepository(AdminBakerDbContext context) : base(context)
    {
    }
}