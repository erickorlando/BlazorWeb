using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Entities.Info;
using AdminBaker.Repositories.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

    public async Task<ICollection<PedidoAuditoriaInfo>> ListAuditAsync()
    {
        var query = Context.Database.GetDbConnection()
            .Query<PedidoAuditoriaInfo>
                (sql: "uspAuditoriaPedidos",
                    commandType: CommandType.StoredProcedure);

        return await Task.FromResult(query.ToList());
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

    public async Task TomarPedidoAsync(int idVendedor, int id, string userName)
    {
        var pedido = await Context.Set<Pedido>().FindAsync(id);
        if (pedido?.VendedorId != null)
        {
            throw new InvalidOperationException("El pedido ya fue tomado por otro vendedor");
        }

        if (pedido != null)
        {
            pedido.VendedorId = idVendedor;
            pedido.Usuario = userName;
            await Context.SaveChangesAsync();
        }
    }

    public async Task CancelarPedidoAsync(int id, string userName)
    {
        var pedido = await Context.Set<Pedido>().FindAsync(id);
        if (pedido != null)
        {
            pedido.EstadoPedido = EstadoPedido.Cancelado;
            pedido.Usuario = userName;
            await Context.SaveChangesAsync();
        }
    }

    public async Task CambiarEstadoAsync(int id, EstadoPedido estado, string userName)
    {
        var pedido = await Context.Set<Pedido>().FindAsync(id);
        if (pedido != null)
        {
            pedido.EstadoPedido = estado;
            pedido.Usuario = userName;
            await Context.SaveChangesAsync();
        }
    }

    public async Task<ICollection<PedidoItem>> GetItemsAsync(int id)
    {
        return await Context.Set<PedidoItem>()
            .Include(p => p.Producto)
            .Where(p => p.PedidoId == id)
            .AsNoTracking()
            .ToListAsync();
    }
}