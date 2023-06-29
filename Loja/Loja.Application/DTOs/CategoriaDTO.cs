using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Loja.Application.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome da categoria")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe uma descrição para a categoria")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe um Url para a imagem")]
        [MinLength(5)]
        [MaxLength(250)]
        public string ImagemUrl { get; set; }
    }
}
