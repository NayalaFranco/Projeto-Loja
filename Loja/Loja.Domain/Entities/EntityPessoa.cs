namespace Loja.Domain.Entities
{
    public abstract class EntityPessoa : Entity
    {
        public string CPF { get; protected set; }
        public DateTime Nascimento { get; protected set; }
        public string Endereco { get; set; }
        public string? Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; protected set; }
        public ICollection<Ordem>? Ordens { get; set; }
    }
}
