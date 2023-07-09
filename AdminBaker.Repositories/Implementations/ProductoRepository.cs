using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Entities.Info;
using AdminBaker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdminBaker.Repositories.Implementations;

public class ProductoRepository : RepositoryBase<Producto>, IProductoRepository
{
    private readonly Expression<Func<Producto, ProductoInfo>> _selector = p => new ProductoInfo
    {
        Id = p.Id,
        TipoTortaId = p.TipoTortaId,
        TipoTorta = p.TipoTorta.Nombre,
        Nombre = p.Nombre,
        Relleno = p.Relleno,
        Cantidad = p.Cantidad,
        Tamanio = p.Tamanio,
        Precio = p.Precio,
        ImagenUrl = p.ImagenUrl,
        Especial = p.Especial
    };

    public ProductoRepository(AdminBakerDbContext context) : base(context)
    {
    }

    public async Task<ICollection<ProductoInfo>> ListAsync(string? filtro)
    {
        var query = Context.Set<Producto>()
            .Where(p => p.Estado);

        if (!string.IsNullOrWhiteSpace(filtro))
            query = query.Where(p => p.Nombre.Contains(filtro));

        return await query
            .Select(_selector)
            .ToListAsync();
    }

    public async Task<ICollection<ProductoInfo>> ListTopCarousel()
    {
        return await Context.Set<Producto>()
            .Where(p => p.Estado && p.Cantidad >= 3)
            .OrderByDescending(p => p.Nombre)
            .Take(5)
            .Select(_selector)
            .ToListAsync();
    }
}