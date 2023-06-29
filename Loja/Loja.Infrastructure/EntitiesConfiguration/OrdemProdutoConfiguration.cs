using Loja.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Infrastructure.EntitiesConfiguration
{
    public class OrdemProdutoConfiguration : IEntityTypeConfiguration<OrdemProduto>
    {
        public void Configure(EntityTypeBuilder<OrdemProduto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(op => op.Produto)
                .WithMany(p => p.Ordens)
                .HasForeignKey(op => op.ProdutoId);

            builder.HasOne(op => op.Ordem)
                .WithMany(o => o.Produtos)
                .HasForeignKey(op => op.OrdemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(op => op.PrecoUnitario).HasPrecision(10, 2).IsRequired();
            builder.Property(op => op.Quantidade).IsRequired();
            builder.Property(op => op.Desconto).HasPrecision(10, 2);
            builder.Property(op => op.NomeProduto).IsRequired();
        }
    }
}
