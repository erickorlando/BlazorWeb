using AdminBaker.Entities;
using AdminBaker.Entities.Info;

namespace AdminBaker.Repositories.Interfaces;

public interface IMateriaPrimaRepository : IRepositoryBase<MateriaPrima>
{
    Task<ICollection<MateriaPrimaInfo>> ListAsync();
    
    Task<ICollection<MateriaPrimaAuditoriaInfo>> ListAuditAsync();
}