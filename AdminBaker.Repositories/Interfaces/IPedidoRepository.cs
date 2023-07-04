using AdminBaker.Entities;

namespace AdminBaker.Repositories.Interfaces;

public interface IPedidoRepository : IRepositoryBase<Pedido>
{
    Task AddItemAsync(PedidoItem item);
}