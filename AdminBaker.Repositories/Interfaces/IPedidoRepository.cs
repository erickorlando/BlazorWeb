using AdminBaker.Entities;
using AdminBaker.Entities.Info;

namespace AdminBaker.Repositories.Interfaces;

public interface IPedidoRepository : IRepositoryBase<Pedido>
{
    Task<ICollection<PedidoInfo>> ListAsync(DateTime fechaInicio, DateTime fechaFin, string? filter);

    Task<ICollection<PedidoAuditoriaInfo>> ListAuditAsync();

    Task AddItemAsync(PedidoItem item);

    Task TomarPedidoAsync(int idVendedor, int id, string userName);

    Task CancelarPedidoAsync(int id, string userName);

    Task CambiarEstadoAsync(int id, EstadoPedido estado, string userName);
    
    Task<ICollection<PedidoItem>> GetItemsAsync(int id);
}