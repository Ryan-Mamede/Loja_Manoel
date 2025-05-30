namespace CaixasAPI.Infrastructure.Entities
{
    public class ProdutoEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public int DimensoesId { get; set; }
        public DimensoesEntity Dimensoes { get; set; } = null!;

        public int? PedidoId { get; set; }
        public PedidoEntity? Pedido { get; set; }

        public int? CaixaId { get; set; }
        public CaixaEntity? Caixa { get; set; }
    }
}
