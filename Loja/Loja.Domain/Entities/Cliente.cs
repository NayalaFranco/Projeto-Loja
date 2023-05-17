namespace Loja.Domain.Entities
{
    public sealed class Cliente : EntityPessoa
    {
        public Cliente(string nome, string cpf, DateTime nascimento, string endereco, string email, string telefone, DateTime dataCadastro)
        {
            Nome = nome;
            CPF = cpf;
            Nascimento = nascimento;
            Endereco = endereco;
            Email = email;
            Telefone = telefone;
            DataCadastro = dataCadastro;
        }

    }
}
