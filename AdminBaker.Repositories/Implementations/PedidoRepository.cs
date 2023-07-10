using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Entities.Info;
using AdminBaker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminBaker.Repositories.Implementations;

public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
{
    public PedidoRepository(AdminBakerDbContext context) : base(context)
    {
    }

    public async Task<ICollection<PedidoInfo>> ListAsync(DateTime fechaInicio, DateTime fechaFin, string? filter)
    {
        var query = Context.Set<PedidoInfo>()
            .FromSql($"EXEC uspListarPedidos {fechaInicio}, {fechaFin}, {filter}");

        return await query
            .ToListAsync();
    }

    public async Task AddItemAsync(PedidoItem item)
    {
        await Context.Set<PedidoItem>().AddAsync(item);
    }

    public override async Task<int> AddAsync(Pedido entity)
    {
        await Context.Set<Pedido>().AddAsync(entity);
        return entity.Id;
    }
}