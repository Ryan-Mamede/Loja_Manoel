using CaixasAPI.Domain.Dtos;
using CaixasAPI.Domain.Models;
using System.Collections.Generic;

namespace CaixasAPI.Domain.Services
{
    public interface IEmpacotamentoService
    {
        List<Pedido> ProcessarPedidos(List<Pedido> pedidos);
        ProcessamentoPedidosSaidaDto MapearParaSaida(List<Pedido> pedidosProcessados);
    }
}
