using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Entities.Info;
using AdminBaker.Repositories.Interfaces;
using Dapper;
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

    public async Task<ICollection<Cliente>> ListAsync(string? filter)
    {
        var query = Context.Set<Cliente>()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            query = query.Where(x => x.NombreCompleto.Contains(filter) || x.Rut.Contains(filter));
        }

        return await query
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<ClienteAuditoriaInfo>> ListAuditAsync()
    {
        var query = Context.Database.GetDbConnection()
            .Query<ClienteAuditoriaInfo>(sql: "uspAuditoriaClientes",
                commandType: System.Data.CommandType.StoredProcedure);
        
        return await Task.FromResult(query.ToList());
    }

    public async Task ReactivarAsync(int id)
    {
        var registro = await Context.Set<Cliente>().FindAsync(id);
        if (registro is not null)
        {
            registro.Estado = true;
            await Context.SaveChangesAsync();
        }
    }
}