using Loja.Domain.Entities;

namespace Loja.TestesUnitarios
{
    internal class DBUnitTestsInitializer
    {
        public DBUnitTestsInitializer()
        {

        }

        public void Seed(ApplicationDbContext context)
        {
            // Categorias
            context.Categorias.Add
                (new Categoria(
                    nome: "Material Escolar",
                    descricao: "Materiais escolar diversos",
                    imagemUrl: "materialEscolarCat.jpg"
                    ));
            context.Categorias.Add
                (new Categoria(
                    nome: "Roupas",
                    descricao: "Roupas Masculinas e Femininas",
                    imagemUrl: "roupasCat.jpg"
                    ));
            context.Categorias.Add
                (new Categoria(
                    nome: "Eletrodomésticos",
                    descricao: "Eletrodomésticos para a sua casa",
                    imagemUrl: "eletrodomesticosCat.jpg"
                    ));
            context.Categorias.Add
                (new Categoria(
                    nome: "ParaExcluir",
                    descricao: "TesteExclusão",
                    imagemUrl: "lixeira.jpg"
                    ));

            // Produtos
            // Cat. Mat. Escolar
            context.Produtos.Add
                (new Produto(
                    nome: "Caderno espiral",
                    descricao: "Caderno espiral 100 folhas",
                    preco: 7.45M,
                    imagemUrl: "caderno1.jpg",
                    estoque: 50,
                    dataCadastro: DateTime.Parse("2023-05-02"),
                    categoriaId: 1
                    ));
            context.Produtos.Add
                (new Produto(
                    nome: "Estojo escolar",
                    descricao: "Estojo escolar cinza",
                    preco: 5.65M,
                    imagemUrl: "estojo1.jpg",
                    estoque: 70,
                    dataCadastro: DateTime.Parse("2023-04-02"),
                    categoriaId: 1));
            context.Produtos.Add
                (new Produto(
                    nome: "Borracha escolar",
                    descricao: "Borracha branca pequena",
                    preco: 3.25M,
                    imagemUrl: "borracha1.jpg",
                    estoque: 80,
                    dataCadastro: DateTime.Parse("2023-03-01"),
                    categoriaId: 1
                    ));
            context.Produtos.Add
                (new Produto(
                    nome: "Calculadora escolar",
                    descricao: "Calculadora simples",
                    preco: 15.39M,
                    imagemUrl: "calculadora1.jpg",
                    estoque: 20,
                    dataCadastro: DateTime.Parse("2023-04-29"),
                    categoriaId: 1
                    ));

            // Cat. Roupas
            context.Produtos.Add
                (new Produto(
                    nome: "Camiseta Polo Masc.",
                    descricao: "Camiseta Polo Masc. Tam.M",
                    preco: 56.90M,
                    imagemUrl: "camisetaPolo1.jpg",
                    estoque: 11,
                    dataCadastro: DateTime.Parse("2023-05-01"),
                    categoriaId: 2
                    ));
            context.Produtos.Add
                (new Produto(
                    nome: "Jaqueta Unissex",
                    descricao: "Jaqueta Unissex Tam.G",
                    preco: 128.90M,
                    imagemUrl: "jaquetaUnissex1.jpg",
                    estoque: 4,
                    dataCadastro: DateTime.Parse("2023-02-11"),
                    categoriaId: 2
                    ));
            context.Produtos.Add
                (new Produto(
                    nome: "Calça Jeans Fem.",
                    descricao: "Calça Jeans Fem. Azul Tam.P",
                    preco: 69.90M,
                    imagemUrl: "jeans1.jpg",
                    estoque: 20,
                    dataCadastro: DateTime.Parse("2023-04-10"),
                    categoriaId: 2
                    ));
            context.Produtos.Add
                (new Produto(
                    nome: "Pct. c/ 5 Meias",
                    descricao: "Pct. c/ 5 meias cores diversas Tam.43",
                    preco: 21.90M,
                    imagemUrl: "pacoteMeia1.jpg",
                    estoque: 25,
                    dataCadastro: DateTime.Parse("2022-10-11"),
                    categoriaId: 2
                    ));

            // Cat. Eletromesticos
            context.Produtos.Add
                (new Produto(
                    nome: "Geladeira",
                    descricao: "Geladeira 110v Branca",
                    preco: 999.90M,
                    imagemUrl: "geladeira2.jpg",
                    estoque: 8,
                    dataCadastro: DateTime.Parse("2023-05-03"),
                    categoriaId: 3
                    ));
            context.Produtos.Add
                (new Produto(
                    nome: "Batedeira",
                    descricao: "Batedeira 220v Verm.",
                    preco: 259.90M,
                    imagemUrl: "batedeiraVerm1.jpg",
                    estoque: 3,
                    dataCadastro: DateTime.Parse("2023-03-20"),
                    categoriaId: 3
                    ));
            context.Produtos.Add
                (new Produto(
                    nome: "Maquina de Lavar",
                    descricao: "Maquina de Lavar 110v Branca",
                    preco: 1990.00M,
                    imagemUrl: "maqLavar.jpg",
                    estoque: 5,
                    dataCadastro: DateTime.Parse("2023-10-11"),
                    categoriaId: 3
                    ));
            context.Produtos.Add
                (new Produto(
                    nome: "Cooktop Elétrico",
                    descricao: "Cooktop elétrico 220v",
                    preco: 2890.90M,
                    imagemUrl: "coocktop1.jpg",
                    estoque: 11,
                    dataCadastro: DateTime.Parse("2023-01-02"),
                    categoriaId: 3
                    ));

            // Vendedores
            context.Vendedores.Add
                (new Vendedor(
                    nome: "Genivaldo Jovaldo",
                    cpf: "565.703.380-99",
                    nascimento: DateTime.Parse("1990-08-20"),
                    endereco: "R.Endereço qualquer. N.00",
                    email: "email@email.com",
                    telefone: "(00) 0-0000-0000",
                    dataCadastro: DateTime.Parse("2022-02-28")
                    ));
            context.Vendedores.Add
                (new Vendedor(
                    nome: "Josenildo Gevildo",
                    cpf: "380.751.090-74",
                    nascimento: DateTime.Parse("1978-01-11"),
                    endereco: "R.Endereço qualquer. N.01",
                    email: "email@email.com",
                    telefone: "(01) 2-3456-7891",
                    dataCadastro: DateTime.Parse("2023-03-23")
                    ));
            context.Vendedores.Add
                (new Vendedor(
                    nome: "Vendedor Atualização",
                    cpf: "987.654.321-09",
                    nascimento: DateTime.Parse("1950-01-02"),
                    endereco: "R.Endereço qualquer. N.06",
                    email: "email@email.com",
                    telefone: "(33) 3-3333-3333",
                    dataCadastro: DateTime.Parse("2020-05-15")
                    ));
            context.Vendedores.Add
                (new Vendedor(
                    nome: "Vendedor a ser excluido",
                    cpf: "987.654.321-09",
                    nascimento: DateTime.Parse("1960-01-02"),
                    endereco: "R. Da Exclusão. N.07",
                    email: "email@email.com",
                    telefone: "(33) 3-3333-3333",
                    dataCadastro: DateTime.Parse("2020-05-15")
                    ));

            // Clientes
            context.Clientes.Add
                (new Cliente(
                    nome: "Amado Amoroso",
                    cpf: "026.122.460-37",
                    nascimento: DateTime.Parse("1978-01-11"),
                    endereco: "R.Endereço qualquer. N.02",
                    email: "email@email.com",
                    telefone: "(01) 2-3456-7891",
                    dataCadastro: DateTime.Parse("2023-08-23")
                    ));
            context.Clientes.Add
                (new Cliente(
                    nome: "Chaplin da Silva",
                    cpf: "218.461.780-61",
                    nascimento: DateTime.Parse("1999-06-12"),
                    endereco: "R.Endereço qualquer. N.04",
                    email: "email@email.com",
                    telefone: "(11) 1-1111-1111",
                    dataCadastro: DateTime.Parse("2021-01-11")
                    ));
            context.Clientes.Add
                (new Cliente(
                    nome: "Senhor Precisa Atualização",
                    cpf: "987.654.321-09",
                    nascimento: DateTime.Parse("1950-01-02"),
                    endereco: "R.Endereço qualquer. N.05",
                    email: "email@email.com",
                    telefone: "(33) 3-3333-3333",
                    dataCadastro: DateTime.Parse("2020-05-15")
                    ));
            context.Clientes.Add
                (new Cliente(
                    nome: "Senhor A Ser Deletado",
                    cpf: "987.654.321-09",
                    nascimento: DateTime.Parse("1960-01-02"),
                    endereco: "R. Da Exclusão. N.06",
                    email: "email@email.com",
                    telefone: "(33) 3-3333-3333",
                    dataCadastro: DateTime.Parse("2020-05-15")
                    ));

            // Ordens
            context.Ordens.Add
                (new Ordem(
                    vendedorId: 1,
                    clienteId: 1,
                    total: 5526.63M,
                    dataCriacao: DateTime.Parse("2023-06-03")
                    ));
            context.Ordens.Add
                (new Ordem(
                    vendedorId: 2,
                    clienteId: 1,
                    total: 164.06M,
                    dataCriacao: DateTime.Parse("2023-05-09")
                    ));
            context.Ordens.Add
                (new Ordem(
                    vendedorId: 2,
                    clienteId: 2,
                    total: 16.35M,
                    dataCriacao: DateTime.Parse("2023-06-03")
                    ));

            // OrdemProduto - Tabela de Junção
            // Ordem - 1
            context.OrdemProdutoJuncao.Add
                (new OrdemProduto(
                    ordemId: 1,
                    produtoId: 9,
                    nomeProduto: "Geladeira 110v Branca",
                    precoUnitario: 999.90M,
                    quantidade: 1,
                    desconto: 99.99M
                    ));
            context.OrdemProdutoJuncao.Add
                (new OrdemProduto(
                    ordemId: 1,
                    produtoId: 10,
                    nomeProduto: "Batedeira 220v Verm.",
                    precoUnitario: 259.90M,
                    quantidade: 1,
                    desconto: 25.99M
                    ));
            context.OrdemProdutoJuncao.Add
                (new OrdemProduto(
                    ordemId: 1,
                    produtoId: 11,
                    nomeProduto: "Maquina de Lavar 110v Branca",
                    precoUnitario: 1990.00M,
                    quantidade: 1,
                    desconto: 199.00M
                    ));
            context.OrdemProdutoJuncao.Add
                (new OrdemProduto(
                    ordemId: 1,
                    produtoId: 12,
                    nomeProduto: "Cooktop elétrico 220v",
                    precoUnitario: 2890.90M,
                    quantidade: 1,
                    desconto: 289.09M
                    ));

            // Ordem - 2
            context.OrdemProdutoJuncao.Add
                (new OrdemProduto(
                    ordemId: 2,
                    produtoId: 8,
                    nomeProduto: "Pct. c/ 5 meias cores diversas Tam.43",
                    precoUnitario: 21.90M,
                    quantidade: 2,
                    desconto: 2.19M
                    ));
            context.OrdemProdutoJuncao.Add
                (new OrdemProduto(
                    ordemId: 2,
                    produtoId: 6,
                    nomeProduto: "Jaqueta Unissex Tam.G",
                    precoUnitario: 128.90M,
                    quantidade: 1,
                    desconto: 6.45M
                    ));


            // Ordem - 3
            context.OrdemProdutoJuncao.Add
                (new OrdemProduto(
                    ordemId: 3,
                    produtoId: 1,
                    nomeProduto: "Caderno espiral 100 folhas",
                    precoUnitario: 7.45M,
                    quantidade: 1,
                    desconto: 0
                    ));
            context.OrdemProdutoJuncao.Add
                (new OrdemProduto(
                    ordemId: 3,
                    produtoId: 2,
                    nomeProduto: "Estojo escolar cinza",
                    precoUnitario: 5.65M,
                    quantidade: 1,
                    desconto: 0
                    ));
            context.OrdemProdutoJuncao.Add
                (new OrdemProduto(
                    ordemId: 3,
                    produtoId: 3,
                    nomeProduto: "Borracha branca pequena",
                    precoUnitario: 3.25M,
                    quantidade: 1,
                    desconto: 0
                    ));

            context.SaveChanges();

            // Para dar Detach em tudo e não ter erro
            // de algo estando trackeado depois.
            context.ChangeTracker.Clear();
        }
    }
}
