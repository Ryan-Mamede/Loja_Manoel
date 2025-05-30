using CaixasAPI.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaixasAPI.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<PedidoEntity> Pedidos { get; set; }
        public DbSet<ProdutoEntity> Produtos { get; set; }
        public DbSet<CaixaEntity> Caixas { get; set; }
        public DbSet<DimensoesEntity> Dimensoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PedidoEntity>(e =>
            {
                e.ToTable("Pedidos");
                e.HasKey(p => p.Id);
                e.Property(p => p.NumeroPedido).IsRequired();
                e.HasMany(p => p.Produtos).WithOne(p => p.Pedido).HasForeignKey(p => p.PedidoId);
                e.HasMany(p => p.Caixas).WithOne(c => c.Pedido).HasForeignKey(c => c.PedidoId);
            });

            modelBuilder.Entity<ProdutoEntity>(e =>
            {
                e.ToTable("Produtos");
                e.HasKey(p => p.Id);
                e.Property(p => p.Nome).IsRequired();
                e.HasOne(p => p.Dimensoes).WithOne(d => d.Produto).HasForeignKey<DimensoesEntity>(d => d.ProdutoId);
                e.HasOne(p => p.Caixa).WithMany(c => c.Produtos).HasForeignKey(p => p.CaixaId);
            });

            modelBuilder.Entity<CaixaEntity>(e =>
            {
                e.ToTable("Caixas");
                e.HasKey(c => c.Id);
                e.Property(c => c.Nome).IsRequired();
                e.HasOne(c => c.Dimensoes).WithOne(d => d.Caixa).HasForeignKey<DimensoesEntity>(d => d.CaixaId);
            });

            modelBuilder.Entity<DimensoesEntity>(e =>
            {
                e.ToTable("Dimensoes");
                e.HasKey(d => d.Id);
                e.Property(d => d.Altura).IsRequired();
                e.Property(d => d.Largura).IsRequired();
                e.Property(d => d.Comprimento).IsRequired();
            });

            modelBuilder.Entity<PedidoEntity>().HasData(
                new PedidoEntity
                {
                    Id = 1,
                    NumeroPedido = "Pedido Inicial"
                }
            );

            modelBuilder.Entity<CaixaEntity>().HasData(
                new CaixaEntity
                {
                    Id = 1,
                    Nome = "Caixa 1",
                    DimensoesId = 1,
                    PedidoId = 1
                },
                new CaixaEntity
                {
                    Id = 2,
                    Nome = "Caixa 2",
                    DimensoesId = 2,
                    PedidoId = 1
                },
                new CaixaEntity
                {
                    Id = 3,
                    Nome = "Caixa 3",
                    DimensoesId = 3,
                    PedidoId = 1,
                }
            );

            modelBuilder.Entity<DimensoesEntity>().HasData(
                new DimensoesEntity
                {
                    Id = 1,
                    Altura = 30,
                    Largura = 40,
                    Comprimento = 80,
                    CaixaId = 1
                },
                new DimensoesEntity
                {
                    Id = 2,
                    Altura = 80,
                    Largura = 50,
                    Comprimento = 40,
                    CaixaId = 2
                },
                new DimensoesEntity
                {
                    Id = 3,
                    Altura = 50,
                    Largura = 80,
                    Comprimento = 60,
                    CaixaId = 3
                }
            );
        }
    }
}
