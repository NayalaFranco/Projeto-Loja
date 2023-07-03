using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Loja.Application.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MinLength(5)]
        [MaxLength(200)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o preço")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [Range(0.01, 99999999.99, ErrorMessage = "O preço deve ser um valor positivo e não pode ser 0.00")]
        public decimal Preco { get; set; }

        [MaxLength(250)]
        public string ImagemUrl { get; set; }

        [Required(ErrorMessage = "O estoque é obrigatório.")]
        [Range(1, 9999, ErrorMessage = "O valor do estoque precisa estar entre 1 e 9999")]
        public int Estoque { get; set; }

        [Required(ErrorMessage = "Informe a data do cadastro.")]
        public DateTime? DataCadastro { get; set; }

        [Required(ErrorMessage = "Informe a ID de uma categoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "A ID da categoria precisa estar entre 1 e 2147483647.")]
        public int CategoriaId { get; set; }
        public CategoriaDTO Categoria { get; set; }
    }
}
