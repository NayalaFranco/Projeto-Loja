using Loja.Domain.Entities;
using Loja.Domain.Enums;

#nullable disable

namespace Loja.Application.DTOs
{
    public class OrdemVendedorDTO
    {
        public int Id { get; }

        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; }

        public List<Produto> ProdutosList { get; set; }
        public EnumStatusVenda StatusVenda { get; set; }
        public DateTime DataCriacao { get; }
    }
}
