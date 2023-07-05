using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;
using System.Linq.Expressions;
using AdminBaker.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace AdminBaker.Repositories.Implementations;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
{
    protected readonly AdminBakerDbContext Context;

    protected RepositoryBase(AdminBakerDbContext context)
    {
        Context = context;
    }

    public virtual async Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>()
            .Where(predicate)
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task<ICollection<TInfo>> ListAsync<TInfo>(Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TInfo>> selector)
    {
        return await Context.Set<TEntity>()
            .Where(predicate)
            //.AsNoTracking() // Cuando se usa Select ya no es necesario el AsNoTracking
            .Select(selector)
            .ToListAsync();
    }

    public virtual async Task<(ICollection<TInfo> Collection, int Total)> ListAsync<TInfo, TKey>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TInfo>> selector,
        Expression<Func<TEntity, TKey>> orderBy,
        int page, int rows)
    {
        var collection = await Context.Set<TEntity>()
            .Where(predicate)
            .OrderBy(orderBy)
            .Skip((page - 1) * rows)
            .Take(rows)
            .Select(selector)
            .ToListAsync();

        var count = await Context.Set<TEntity>()
            .Where(predicate)
            .CountAsync();

        return (collection, count);
    }

    public virtual async Task<int> AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity.Id;
    }


    public virtual async Task<TEntity?> FindByIdAsync(int id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task UpdateAsync()
    {
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await FindByIdAsync(id);
        if (entity != null)
        {
            entity.Estado = false;
            await UpdateAsync();
        }
        else
        {
            throw new InvalidOperationException($"No se encontro el registro con el Id {id}");
        }
    }

}