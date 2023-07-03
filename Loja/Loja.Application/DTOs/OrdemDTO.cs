using Loja.Domain.Enums;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Loja.Application.DTOs
{
    public class OrdemDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Id do vendedor é obrigatório!")]
        [Range(1, int.MaxValue, ErrorMessage = "A ID do vendedor precisa estar entre 1 e 2147483647.")]
        public int VendedorId { get; set; }

        [Required(ErrorMessage = "Id do cliente é obrigatório!")]
        [Range(1, int.MaxValue, ErrorMessage = "A ID do cliente precisa estar entre 1 e 2147483647.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "É necessário adicionar ao menos 1 produto!")]
        public IList<OrdemProdutoDTO> Produtos { get; set; }

        public decimal Total { get; set; }
        public EnumStatusVenda StatusVenda { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
