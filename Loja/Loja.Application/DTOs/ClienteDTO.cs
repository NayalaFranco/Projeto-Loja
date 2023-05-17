using Loja.Application.Validations;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Loja.Application.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [CpfValidation]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Informe a data de nascimento")]
        public DateTime Nascimento { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        [MinLength(5)]
        [MaxLength(200)]
        public string Endereco { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o telefone")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe a data do cadastro")]
        public DateTime DataCadastro { get; set; }

    }
}
