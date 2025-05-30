using CaixasAPI.Domain.Models;
using CaixasAPI.Infrastructure.Data;
using CaixasAPI.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaixasAPI.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> ObterTodosPedidosAsync()
        {
            var pedidosEntities = await _context.Pedidos
                .Include(p => p.Produtos).ThenInclude(p => p.Dimensoes)
                .Include(p => p.Caixas).ThenInclude(c => c.Dimensoes)
                .Include(p => p.Caixas).ThenInclude(c => c.Produtos).ThenInclude(p => p.Dimensoes)
                .ToListAsync();

            return pedidosEntities.Select(MapearParaModelo).ToList();
        }

        public async Task<Pedido?> ObterPedidoPorIdAsync(int id)
        {
            var pedidoEntity = await _context.Pedidos
                .Include(p => p.Produtos).ThenInclude(p => p.Dimensoes)
                .Include(p => p.Caixas).ThenInclude(c => c.Dimensoes)
                .Include(p => p.Caixas).ThenInclude(c => c.Produtos).ThenInclude(p => p.Dimensoes)
                .FirstOrDefaultAsync(p => p.Id == id);

            return pedidoEntity != null ? MapearParaModelo(pedidoEntity) : null;
        }

        public async Task<Pedido?> ObterPedidoPorNumeroAsync(string numeroPedido)
        {
            var pedidoEntity = await _context.Pedidos
                .Include(p => p.Produtos).ThenInclude(p => p.Dimensoes)
                .Include(p => p.Caixas).ThenInclude(c => c.Dimensoes)
                .Include(p => p.Caixas).ThenInclude(c => c.Produtos).ThenInclude(p => p.Dimensoes)
                .FirstOrDefaultAsync(p => p.NumeroPedido == numeroPedido);

            return pedidoEntity != null ? MapearParaModelo(pedidoEntity) : null;
        }

        public async Task<Pedido> SalvarPedidoAsync(Pedido pedido)
        {
            var pedidoEntity = MapearParaEntity(pedido);

            if (pedido.Id == 0)
            {
                
                _context.Pedidos.Add(pedidoEntity);
            }
            else
            {
                
                var existingPedido = await _context.Pedidos
                    .Include(p => p.Produtos).ThenInclude(p => p.Dimensoes)
                    .Include(p => p.Caixas).ThenInclude(c => c.Dimensoes)
                    .Include(p => p.Caixas).ThenInclude(c => c.Produtos)
                    .FirstOrDefaultAsync(p => p.Id == pedido.Id);

                if (existingPedido == null)
                {
                    throw new KeyNotFoundException($"Pedido com ID {pedido.Id} não encontrado");
                }

                
                _context.Produtos.RemoveRange(existingPedido.Produtos);
                _context.Caixas.RemoveRange(existingPedido.Caixas);

                
                existingPedido.NumeroPedido = pedidoEntity.NumeroPedido;

                
                foreach (var produto in pedidoEntity.Produtos)
                {
                    existingPedido.Produtos.Add(produto);
                }

                foreach (var caixa in pedidoEntity.Caixas)
                {
                    existingPedido.Caixas.Add(caixa);
                }

                _context.Pedidos.Update(existingPedido);
            }

            await _context.SaveChangesAsync();

            var pedidoSalvo = await ObterPedidoPorIdAsync(pedidoEntity.Id);
            return pedidoSalvo!;
        }

        public async Task<bool> DeletarPedidoAsync(int id)
        {
            var pedidoEntity = await _context.Pedidos
                .Include(p => p.Produtos).ThenInclude(p => p.Dimensoes)
                .Include(p => p.Caixas).ThenInclude(c => c.Dimensoes)
                .Include(p => p.Caixas).ThenInclude(c => c.Produtos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedidoEntity == null)
            {
                return false;
            }

            _context.Produtos.RemoveRange(pedidoEntity.Produtos);
            _context.Caixas.RemoveRange(pedidoEntity.Caixas);
            _context.Pedidos.Remove(pedidoEntity);

            await _context.SaveChangesAsync();
            return true;
        }

        private static Pedido MapearParaModelo(PedidoEntity pedidoEntity)
        {
            var pedido = new Pedido
            {
                Id = pedidoEntity.Id,
                NumeroPedido = pedidoEntity.NumeroPedido,
                Produtos = pedidoEntity.Produtos.Select(p => new Produto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Dimensoes = new Dimensoes
                    {
                        Altura = p.Dimensoes.Altura,
                        Largura = p.Dimensoes.Largura,
                        Comprimento = p.Dimensoes.Comprimento
                    }
                }).ToList(),
                CaixasUtilizadas = pedidoEntity.Caixas.Select(c => new Caixa
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Dimensoes = new Dimensoes
                    {
                        Altura = c.Dimensoes.Altura,
                        Largura = c.Dimensoes.Largura,
                        Comprimento = c.Dimensoes.Comprimento
                    },
                    ProdutosContidos = c.Produtos.Select(p => new Produto
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Dimensoes = new Dimensoes
                        {
                            Altura = p.Dimensoes.Altura,
                            Largura = p.Dimensoes.Largura,
                            Comprimento = p.Dimensoes.Comprimento
                        }
                    }).ToList()
                }).ToList()
            };

            return pedido;
        }

        private static PedidoEntity MapearParaEntity(Pedido pedido)
        {
            var pedidoEntity = new PedidoEntity
            {
                Id = pedido.Id,
                NumeroPedido = pedido.NumeroPedido,
                Produtos = pedido.Produtos.Select(p => new ProdutoEntity
                {
                    Id = p.Id != 0 ? p.Id : 0,
                    Nome = p.Nome,
                    Dimensoes = new DimensoesEntity
                    {
                        Altura = p.Dimensoes.Altura,
                        Largura = p.Dimensoes.Largura,
                        Comprimento = p.Dimensoes.Comprimento
                    }
                }).ToList(),
                Caixas = pedido.CaixasUtilizadas.Select(c => new CaixaEntity
                {
                    Id = c.Id != 0 ? c.Id : 0,
                    Nome = c.Nome,
                    Dimensoes = new DimensoesEntity
                    {
                        Altura = c.Dimensoes.Altura,
                        Largura = c.Dimensoes.Largura,
                        Comprimento = c.Dimensoes.Comprimento
                    },
                    Produtos = c.ProdutosContidos.Select(p => new ProdutoEntity
                    {
                        Id = p.Id != 0 ? p.Id : 0,
                        Nome = p.Nome,
                        Dimensoes = new DimensoesEntity
                        {
                            Altura = p.Dimensoes.Altura,
                            Largura = p.Dimensoes.Largura,
                            Comprimento = p.Dimensoes.Comprimento
                        }
                    }).ToList()
                }).ToList()
            };

            return pedidoEntity;
        }
    }
}
