using CaixasAPI.Domain.Dtos;
using CaixasAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CaixasAPI.Domain.Services
{
    public class EmpacotamentoService : IEmpacotamentoService
    {
        private readonly List<Caixa> _caixasPredefinidas;

        public EmpacotamentoService()
        {
            
            _caixasPredefinidas = new List<Caixa>
            {
                new Caixa("Caixa 1", 30, 40, 80),
                new Caixa("Caixa 2", 80, 50, 40),
                new Caixa("Caixa 3", 50, 80, 60)
            };
        }

        public List<Pedido> ProcessarPedidos(List<Pedido> pedidos)
        {
            foreach (var pedido in pedidos)
            {
                
                var produtosOrdenados = pedido.Produtos.OrderByDescending(p => p.Dimensoes.Volume).ToList();
                
                var caixasUtilizadas = new List<Caixa>();

                
                foreach (var produto in produtosOrdenados)
                {
                    
                    bool alocado = false;

                    foreach (var caixa in caixasUtilizadas)
                    {
                        if (caixa.PodeAcomodarProduto(produto))
                        {
                            caixa.AdicionarProduto(produto);
                            alocado = true;
                            break;
                        }


                    }



                    if (!alocado)
                    {
                        
                        var novaCaixa = EncontrarCaixaIdeal(produto);
                        if (novaCaixa != null)
                        {
                            novaCaixa.AdicionarProduto(produto);
                            caixasUtilizadas.Add(novaCaixa);
                        }
                        else
                        {
                            
                            var maiorCaixa = new Caixa(_caixasPredefinidas.OrderByDescending(c => c.Dimensoes.Volume).First().Nome,
                                                       _caixasPredefinidas.OrderByDescending(c => c.Dimensoes.Volume).First().Dimensoes.Altura,
                                                       _caixasPredefinidas.OrderByDescending(c => c.Dimensoes.Volume).First().Dimensoes.Largura,
                                                       _caixasPredefinidas.OrderByDescending(c => c.Dimensoes.Volume).First().Dimensoes.Comprimento);
                            maiorCaixa.AdicionarProduto(produto);
                            caixasUtilizadas.Add(maiorCaixa);
                        }
                    }

                    
                    foreach (var caixa in caixasUtilizadas)
                    {
                        caixa.PedidoId = pedido.Id;
                    }
                         pedido.CaixasUtilizadas = caixasUtilizadas;
                }

                pedido.CaixasUtilizadas = caixasUtilizadas;
            }

            return pedidos;
        }

        public ProcessamentoPedidosSaidaDto MapearParaSaida(List<Pedido> pedidosProcessados)
        {
            var resultado = new ProcessamentoPedidosSaidaDto
            {
                Pedidos = pedidosProcessados.Select(p => new PedidoSaidaDto
                {
                    NumeroPedido = p.NumeroPedido,
                    Caixas = p.CaixasUtilizadas.Select(c => new CaixaSaidaDto
                    {
                        Nome = c.Nome,
                        Dimensoes = new DimensoesDto
                        {
                            Altura = c.Dimensoes.Altura,
                            Largura = c.Dimensoes.Largura,
                            Comprimento = c.Dimensoes.Comprimento
                        },

                        
                        Produtos = c.ProdutosContidos.Select(prod => new ProdutoNaCaixaDto
                        {
                            Nome = prod.Nome,
                            Dimensoes = new DimensoesDto
                            {
                                Altura = prod.Dimensoes.Altura,
                                Largura = prod.Dimensoes.Largura,
                                Comprimento = prod.Dimensoes.Comprimento
                            }
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            return resultado;
        }

        private Caixa? EncontrarCaixaIdeal(Produto produto)
        {
            
            var caixasAdequadas = _caixasPredefinidas
                .Where(c => produto.PossiveisOrientacoes().Any(o =>
                    o.Altura <= c.Dimensoes.Altura &&
                    o.Largura <= c.Dimensoes.Largura &&
                    o.Comprimento <= c.Dimensoes.Comprimento))
                .OrderBy(c => c.Dimensoes.Volume)
                .ToList();

            if (caixasAdequadas.Any())
            {
                var caixaIdeal = caixasAdequadas.First();
                return new Caixa(caixaIdeal.Nome, caixaIdeal.Dimensoes.Altura, caixaIdeal.Dimensoes.Largura, caixaIdeal.Dimensoes.Comprimento);
            }

            return null;
        }
    }
}
