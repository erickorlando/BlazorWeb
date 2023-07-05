using AdminBaker.Entities;
using AdminBaker.Entities.Info;

namespace AdminBaker.Repositories.Interfaces;

public interface IPedidoRepository : IRepositoryBase<Pedido>
{
    Task<ICollection<PedidoInfo>> ListAsync(string? filter);

    Task AddItemAsync(PedidoItem item);
}