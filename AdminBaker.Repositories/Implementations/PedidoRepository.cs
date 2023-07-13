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

    public async Task<ICollection<PedidoAuditoriaInfo>> ListAuditAsync()
    {
        var query = Context.Set<Pedido>()
            .TemporalAsOf(DateTime.UtcNow)
            .OrderByDescending(p => p.Id)
            .Take(50)
            .Select(p => new PedidoAuditoriaInfo
            {
                Id = p.Id,
                NroPedido = p.NroPedido,
                Fecha = p.Fecha,
                Cliente = p.Cliente.NombreCompleto,
                EstadoPedido = p.EstadoPedido,
                Vendedor = p.Vendedor != null ? p.Vendedor.NombreCompleto : null,
                FechaCambio = EF.Property<DateTime>(p, "PeriodEnd")
            });

        return await query.ToListAsync();
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

    public async Task TomarPedidoAsync(int idVendedor, int id)
    {
        var pedido = await Context.Set<Pedido>().FindAsync(id);
        if (pedido?.VendedorId != null)
        {
            throw new InvalidOperationException("El pedido ya fue tomado por otro vendedor");
        }

        if (pedido != null)
        {
            pedido.VendedorId = idVendedor;
            await Context.SaveChangesAsync();
        }
    }

    public async Task CancelarPedidoAsync(int id)
    {
        var pedido = await Context.Set<Pedido>().FindAsync(id);
        if (pedido != null)
        {
            pedido.EstadoPedido = EstadoPedido.Cancelado;
            await Context.SaveChangesAsync();
        }
    }

    public async Task CambiarEstadoAsync(int id, EstadoPedido estado)
    {
        var pedido = await Context.Set<Pedido>().FindAsync(id);
        if (pedido != null)
        {
            pedido.EstadoPedido = estado;
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