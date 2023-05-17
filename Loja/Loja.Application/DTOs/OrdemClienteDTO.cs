using Loja.Domain.Entities;
using Loja.Domain.Enums;

#nullable disable

namespace Loja.Application.DTOs
{
    public class OrdemClienteDTO
    {
        public int Id { get; }
        public int ClienteId { get; }
        public Cliente Cliente { get; }
        public List<Produto> ProdutosList { get; set; }
        public EnumStatusVenda StatusVenda { get; }
    }
}
