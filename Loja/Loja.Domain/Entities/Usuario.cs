namespace Loja.Domain.Entities
{
    public abstract class Usuario : Entity
    {
        public string Cpf { get; protected set; }
        public DateTime Nascimento { get; protected set; }
        public string Endereco { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public ICollection<Ordem>? Ordens { get; set; }
        public DateTime DataCadastro { get; protected set; }
    }
}
