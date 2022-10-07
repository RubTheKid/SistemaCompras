using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaCompra.Domain.Core;
using SistemaCompra.Infra.Data.Produto;
using SistemaCompra.Infra.Data.SolicitacaoCompra;
using ProdutoAggregate = SistemaCompra.Domain.ProdutoAggregate;
using SolicitacaoCompraAggregate = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data
{
    public class SistemaCompraContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public SistemaCompraContext(DbContextOptions options) : base(options) { }
        public DbSet<ProdutoAggregate.Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SolicitacaoCompraAggregate.SolicitacaoCompra>()
                .OwnsOne(a => a.UsuarioSolicitante)
                .Property(a => a.Nome)
                .HasColumnName("UsuarioSolicitante");
            modelBuilder.Entity<SolicitacaoCompraAggregate.SolicitacaoCompra>()
                .OwnsOne(a => a.NomeFornecedor)
                .Property(a => a.Nome)
                .HasColumnName("NomeFornecedor");
            modelBuilder.Entity<SolicitacaoCompraAggregate.SolicitacaoCompra>()
               .Ignore(a => a.CondicaoPagamento);
            modelBuilder.Entity<SolicitacaoCompraAggregate.Item>()
                        .HasOne(s => s.SolicitacaoCompra)
                        .WithMany()
                        .HasForeignKey("SolicitacaoCompraId");
            modelBuilder.Entity<SolicitacaoCompraAggregate.Item>()
                        .HasOne(i => i.Produto)
                        .WithMany()
                        .HasForeignKey("ProdutoId");
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new SolicitacaoCompraConfiguration());
            modelBuilder.Ignore<Event>();
        }
    }
}
