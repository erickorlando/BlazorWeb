using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Entities.Info;
using AdminBaker.Repositories.Interfaces;
using Dapper;
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

    public async Task<ICollection<MateriaPrimaAuditoriaInfo>> ListAuditAsync()
    {
        var query = Context.Database.GetDbConnection()
            .Query<MateriaPrimaAuditoriaInfo>(sql: "uspAuditoriaMateriaPrimas",
                commandType: System.Data.CommandType.StoredProcedure);
        
        return await Task.FromResult(query.ToList());
    }
}