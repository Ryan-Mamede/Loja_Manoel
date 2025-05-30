using System.Collections.Generic;

namespace CaixasAPI.Infrastructure.Entities
{
    public class CaixaEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public int DimensoesId { get; set; }
        public DimensoesEntity Dimensoes { get; set; } = null!;

        public List<ProdutoEntity> Produtos { get; set; } = new List<ProdutoEntity>();

        public int PedidoId { get; set; }
        public PedidoEntity Pedido { get; set; } = null!;
    }
}
