using System;

namespace CaixasAPI.Domain.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public Dimensoes Dimensoes { get; set; } = new Dimensoes();

        public Dimensoes[] PossiveisOrientacoes()
        {
            return new[] {
                new Dimensoes { Altura = Dimensoes.Altura, Largura = Dimensoes.Largura, Comprimento = Dimensoes.Comprimento },
                new Dimensoes { Altura = Dimensoes.Altura, Largura = Dimensoes.Comprimento, Comprimento = Dimensoes.Largura },
                new Dimensoes { Altura = Dimensoes.Largura, Largura = Dimensoes.Altura, Comprimento = Dimensoes.Comprimento },
                new Dimensoes { Altura = Dimensoes.Largura, Largura = Dimensoes.Comprimento, Comprimento = Dimensoes.Altura },
                new Dimensoes { Altura = Dimensoes.Comprimento, Largura = Dimensoes.Altura, Comprimento = Dimensoes.Largura },
                new Dimensoes { Altura = Dimensoes.Comprimento, Largura = Dimensoes.Largura, Comprimento = Dimensoes.Altura }
            };
        }
    }
}
