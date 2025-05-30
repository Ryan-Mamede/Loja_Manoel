using System.Collections.Generic;

namespace CaixasAPI.Domain.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string NumeroPedido { get; set; } = string.Empty;
        public List<Produto> Produtos { get; set; } = new List<Produto>();
        public List<Caixa> CaixasUtilizadas { get; set; } = new List<Caixa>();
    }
}
