using Loja.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Infrastructure.EntitiesConfiguration
{
    public class VendedorConfiguration : IEntityTypeConfiguration<Vendedor>
    {
        public void Configure(EntityTypeBuilder<Vendedor> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Nome).HasMaxLength(128).IsRequired();
            builder.Property(p => p.CPF).HasMaxLength(14).IsRequired();
            builder.Property(p => p.Nascimento).IsRequired();
            builder.Property(p => p.Endereco).HasMaxLength(128);
            builder.Property(p => p.Email).HasMaxLength(128);
            builder.Property(p => p.Telefone).HasMaxLength(32).IsRequired();
            builder.Property(p => p.DataCadastro).IsRequired();
        }
    }
}
