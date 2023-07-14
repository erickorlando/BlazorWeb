using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy;

public interface IMateriaPrimaProxy : ICrudRestHelper<MateriaPrimaDtoRequest, MateriaPrimaDto>
{
    Task<ICollection<MateriaPrimaAuditoriaDto>> ListAuditAsync();

}