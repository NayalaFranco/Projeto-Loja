namespace Loja.Domain.Entities
{
    public sealed class Cliente : Usuario
    {
        public Cliente(string nome, string cpf, DateTime nascimento,
            string endereco, string email, string telefone,
            DateTime? dataCadastro)
        {
            Nome = nome;
            Cpf = cpf;
            Nascimento = nascimento;
            Endereco = endereco;
            Email = email;
            Telefone = telefone;
            DataCadastro = (dataCadastro == null) ? DateTime.Now : dataCadastro;
        }

    }
}
