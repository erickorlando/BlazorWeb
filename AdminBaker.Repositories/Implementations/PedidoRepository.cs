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

    public async Task<ICollection<PedidoInfo>> ListAsync(string? filter)
    {
        var query = Context.Set<Pedido>()
            .Where(p => p.Estado);

        if (!string.IsNullOrWhiteSpace(filter))
            query = query.Where(p => p.Cliente.NombreCompleto.Contains(filter));

        return await query
            .Select(p => new PedidoInfo
            {
                Id = p.Id,
                Cliente = p.Cliente.NombreCompleto,
                FechaRetiro = p.FechaRetiro,
                Fecha = p.Fecha,
                UrlImagen = p.UrlImagen,
                TotalVenta = p.TotalVenta,
                TipoPedido = p.TipoPedido,
                EstadoPedido = p.EstadoPedido,
                ClienteId = p.ClienteId,
                VendedorId = p.VendedorId
            })
            .OrderByDescending(p => p.Fecha)
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