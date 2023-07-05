using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Entities.Info;
using AdminBaker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminBaker.Repositories.Implementations;

public class MateriaPrimaRepository : RepositoryBase<MateriaPrima>, IMateriaPrimaRepository
{
    public MateriaPrimaRepository(AdminBakerDbContext context) : base(context)
    {
    }

    public async Task<ICollection<MateriaPrimaInfo>> ListAsync()
    {
        return await Context.Set<MateriaPrima>()
            .Include(x => x.UnidadMedida)
            .Where(p => p.Estado)
            .Select(x => new MateriaPrimaInfo
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Cantidad = x.Cantidad,
                UnidadMedida = x.UnidadMedida.Descripcion,
                Caducidad = x.Caducidad,
                UnidadMedidaId = x.UnidadMedidaId
            })
            .ToListAsync();
    }
}