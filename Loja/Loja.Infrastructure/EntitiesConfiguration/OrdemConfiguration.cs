using Loja.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Infrastructure.EntitiesConfiguration
{
    internal class OrdemConfiguration : IEntityTypeConfiguration<Ordem>
    {
        public void Configure(EntityTypeBuilder<Ordem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(v => v.Vendedor)
                .WithMany(v => v.Ordens)
                .HasForeignKey(v => v.VendedorId);

            builder.HasOne(c => c.Cliente)
                .WithMany(c => c.Ordens)
                .HasForeignKey(c => c.ClienteId);

            builder.Property(p => p.ProdutosList).IsRequired();
            builder.Property(p => p.StatusVenda).IsRequired();
            builder.Property(p => p.DataCriacao).IsRequired();
        }
    }
}
