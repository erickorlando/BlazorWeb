﻿using AdminBaker.DataAccess;
using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminBaker.Repositories.Implementations;

public class VendedorRepository : RepositoryBase<Vendedor>, IVendedorRepository
{
    public VendedorRepository(AdminBakerDbContext context) : base(context)
    {
    }

    public async Task<Vendedor?> FindByEmailAsync(string email)
    {
        return await Context.Set<Vendedor>().FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<ICollection<Vendedor>> ListAsync(string? filter)
    {
        var query = Context.Set<Vendedor>()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            query = query.Where(x => x.NombreCompleto.Contains(filter) || x.Rut.Contains(filter));
        }

        return await query
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task ReactivarAsync(int id)
    {
        var registro = await Context.Set<Vendedor>().FindAsync(id);
        if (registro is not null)
        {
            registro.Estado = true;
            await Context.SaveChangesAsync();
        }
    }
}