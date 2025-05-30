namespace CaixasAPI.Infrastructure.Entities
{
    public class DimensoesEntity
    {
        public int Id { get; set; }
        public int Altura { get; set; }
        public int Largura { get; set; }
        public int Comprimento { get; set; }

        public int? ProdutoId { get; set; }
        public ProdutoEntity? Produto { get; set; }

        public int? CaixaId { get; set; }
        public CaixaEntity? Caixa { get; set; }
    }
}
