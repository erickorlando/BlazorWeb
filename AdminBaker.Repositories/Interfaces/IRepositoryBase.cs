using System.Linq.Expressions;
using AdminBaker.Entities;

namespace AdminBaker.Repositories.Interfaces;

public interface IRepositoryBase<TEntity> where TEntity : EntityBase
{
    // Listar de objetos basados en el EntityBase
    Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);

    // Lista de objetos con un selector
    Task<ICollection<TInfo>> ListAsync<TInfo>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TInfo>> selector);

    // Lista de objetos con paginacion
    Task<(ICollection<TInfo> Collection, int Total)> ListAsync<TInfo, TKey>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TInfo>> selector,
        Expression<Func<TEntity, TKey>> orderBy,
        int page, int rows);

    // Agregar

    Task<int> AddAsync(TEntity entity);

    // Obtener por ID
    Task<TEntity?> FindByIdAsync(int id);

    // Actualizar
    Task UpdateAsync();

    // Eliminar
    Task DeleteAsync(int id);
}