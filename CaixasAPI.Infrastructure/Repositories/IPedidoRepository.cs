using CaixasAPI.Domain.Models;
using CaixasAPI.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaixasAPI.Infrastructure.Repositories
{
    public interface IPedidoRepository
    {
        Task<List<Pedido>> ObterTodosPedidosAsync();
        Task<Pedido?> ObterPedidoPorIdAsync(int id);
        Task<Pedido?> ObterPedidoPorNumeroAsync(string numeroPedido);
        Task<Pedido> SalvarPedidoAsync(Pedido pedido);
        Task<bool> DeletarPedidoAsync(int id);
    }
}
