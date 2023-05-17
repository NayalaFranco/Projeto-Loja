using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Loja.Application.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; }

        [Required(ErrorMessage = "Informe o nome da categoria")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe um Url para a imagem")]
        [MinLength(5)]
        [MaxLength(250)]
        public string ImagemUrl { get; set; }
    }
}
