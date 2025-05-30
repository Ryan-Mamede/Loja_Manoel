namespace CaixasAPI.Domain.Dtos
{
    public class DimensoesDto
    {
        public int Altura { get; set; }
        public int Largura { get; set; }
        public int Comprimento { get; set; }
    }

    public class ProdutoDto
    {
        public string Nome { get; set; } = string.Empty;
        public DimensoesDto Dimensoes { get; set; } = new DimensoesDto();
    }

    public class PedidoEntradaDto
    {
        public string NumeroPedido { get; set; } = string.Empty;
        public List<ProdutoDto> Produtos { get; set; } = new List<ProdutoDto>();
    }

    public class ProdutoNaCaixaDto
    {
        public string Nome { get; set; } = string.Empty;
        public DimensoesDto Dimensoes { get; set; } = new DimensoesDto();
    }

    public class CaixaSaidaDto
    {
        public string Nome { get; set; } = string.Empty;
        public DimensoesDto Dimensoes { get; set; } = new DimensoesDto();
        public List<ProdutoNaCaixaDto> Produtos { get; set; } = new List<ProdutoNaCaixaDto>();
    }

    public class PedidoSaidaDto
    {
        public string NumeroPedido { get; set; } = string.Empty;
        public List<CaixaSaidaDto> Caixas { get; set; } = new List<CaixaSaidaDto>();
    }

    public class ProcessamentoPedidosEntradaDto
    {
        public List<PedidoEntradaDto> Pedidos { get; set; } = new List<PedidoEntradaDto>();
    }

    public class ProcessamentoPedidosSaidaDto
    {
        public List<PedidoSaidaDto> Pedidos { get; set; } = new List<PedidoSaidaDto>();
    }
}
