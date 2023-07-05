using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy;

public interface IRecetaProxy : ICrudRestHelper<RecetaDtoRequest, RecetaDto>
{
    
}