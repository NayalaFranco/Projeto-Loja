using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Loja.Application.DTOs
{
    public class OrdemProdutoDTO
    {
        public int Id { get; set; }
        public int OrdemId { get; set; }

        [Required(ErrorMessage = "Id do produto é obrigatória!")]
        [Range(1, int.MaxValue, ErrorMessage = "A Id do produto precisa estar entre 1 e 2147483647.")]
        public int ProdutoId { get; set; }
        public ProdutoDTO Produto { get; set; }
        public string NomeProduto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        public decimal PrecoUnitario { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória!")]
        [Range(1, 9999, ErrorMessage = "A quantidade deve ser maior que 0")]
        public int Quantidade { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [Range(0.00, 99999999.99, ErrorMessage = "O desconto deve ser um valor positivo.")]
        public decimal Desconto { get; set; }
    }
}
