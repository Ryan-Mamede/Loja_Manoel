using System.Collections.Generic;

namespace CaixasAPI.Domain.Models
{
    public class Caixa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public Dimensoes Dimensoes { get; set; } = new Dimensoes();
        public List<Produto> ProdutosContidos { get; set; } = new List<Produto>();
        public int EspacoDisponivel { get; private set; }
        public int PedidoId { get; set; }


        public Caixa()
        {
        }

        public Caixa(string nome, int altura, int largura, int comprimento)
        {
            Nome = nome;
            Dimensoes = new Dimensoes
            {
                Altura = altura,
                Largura = largura,
                Comprimento = comprimento
            };
            EspacoDisponivel = altura * largura * comprimento;
        }

        public bool PodeAcomodarProduto(Produto produto)
        {
            
            foreach (var orientacao in produto.PossiveisOrientacoes())
            {
                if (CabeNaCaixa(orientacao) && orientacao.Volume <= EspacoDisponivel)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CabeNaCaixa(Dimensoes dimensoesProduto)
        {
            
            return (dimensoesProduto.Altura <= Dimensoes.Altura &&
                    dimensoesProduto.Largura <= Dimensoes.Largura &&
                    dimensoesProduto.Comprimento <= Dimensoes.Comprimento);
        }

        public void AdicionarProduto(Produto produto)
        {
            ProdutosContidos.Add(produto);
            EspacoDisponivel -= produto.Dimensoes.Volume;
        }
    }
}
