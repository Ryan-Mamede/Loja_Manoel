using System.Collections.Generic;

namespace CaixasAPI.Infrastructure.Entities
{
    public class PedidoEntity
    {
        public int Id { get; set; }
        public string NumeroPedido { get; set; } = string.Empty;
        public List<ProdutoEntity> Produtos { get; set; } = new List<ProdutoEntity>();
        public List<CaixaEntity> Caixas { get; set; } = new List<CaixaEntity>();
    }
}
