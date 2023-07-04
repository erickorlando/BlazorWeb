using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;

namespace AdminBaker.Repositories.Implementations;

public class UnidadMedidaRepository : RepositoryBase<UnidadMedida>, IUnidadMedidaRepository
{
    public UnidadMedidaRepository(AdminBakerDbContext context) : base(context)
    {
    }
}