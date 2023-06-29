using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loja.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PopulaDbExemplo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            // Categorias
            mb.Sql("INSERT INTO Categorias(Nome,Descricao,ImagemUrl) " +
                "VALUES('Material Escolar','Materiais escolar diversos','materialEscolarCat.jpg')");
            mb.Sql("INSERT INTO Categorias(Nome,Descricao,ImagemUrl) " +
                "VALUES('Roupas','Roupas Masculinas e Femininas','roupasCat.jpg')");
            mb.Sql("INSERT INTO Categorias(Nome,Descricao,ImagemUrl) " +
                "VALUES('Eletrodomésticos','Eletrodomésticos para a sua casa','eletrodomesticosCat.jpg')");

            // Produtos
            // Cat. Mat. Escolar
            mb.Sql("INSERT INTO Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
            "VALUES('Caderno espiral','Caderno espiral 100 folhas',7.45,'caderno1.jpg',50,'2023-05-02',1)");

            mb.Sql("INSERT INTO Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
            "VALUES('Estojo escolar','Estojo escolar cinza',5.65,'estojo1.jpg',70,'2023-04-02',1)");

            mb.Sql("INSERT INTO Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
            "VALUES('Borracha escolar','Borracha branca pequena',3.25,'borracha1.jpg',80,'2023-03-01',1)");

            mb.Sql("INSERT INTO Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
            "VALUES('Calculadora escolar','Calculadora simples',15.39,'calculadora1.jpg',20,'2023-04-29',1)");

            // Cat. Roupas
            mb.Sql("INSERT INTO Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
            "VALUES('Camiseta Polo Masc.','Camiseta Polo Masc. Tam.M',56.90,'camisetaPolo1.jpg',11,'2023-05-01',2)");

            mb.Sql("INSERT INTO Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
            "VALUES('Jaqueta Unissex','Jaqueta Unissex Tam.G',128.90,'jaquetaUnissex1.jpg',4,'2023-02-11',2)");

            mb.Sql("INSERT INTO Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
            "VALUES('Calça Jeans Fem.','Calça Jeans Fem. Azul Tam.P',69.90,'jeans1.jpg',20,'2023-04-10',2)");

            mb.Sql("INSERT INTO Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
            "VALUES('Pct. c/ 5 Meias','Pct. c/ 5 meias cores diversas Tam.43',21.90,'pacoteMeia1.jpg',25,'2022-10-11',2)");

            // Cat. Eletrodomesticos
            mb.Sql("INSERT INTO Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
            "VALUES('Geladeira','Geladeira 110v Branca',999.90,'geladeira2.jpg',8,'2023-05-03',3)");

            mb.Sql("INSERT INTO Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
            "VALUES('Batedeira','Batedeira 220v Verm.',259.90,'batedeiraVerm1.jpg',3,'2023-03-20',3)");

            mb.Sql("INSERT INTO Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
            "VALUES('Maquina de Lavar','Maquina de Lavar 110v Branca',1990.00,'maqLavar.jpg',5,'2023-10-11',3)");

            mb.Sql("INSERT INTO Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
            "VALUES('Cooktop Elétrico','Cooktop elétrico 220v',2890.90,'coocktop1.jpg',11,'2023-01-02',3)");

            // Vendedores
            mb.Sql("INSERT INTO Vendedores(Nome,Cpf,Nascimento,Endereco,Email,Telefone,DataCadastro) " +
                "VALUES('Genivaldo Jovaldo','565.703.380-99','1990-08-20','R.Endereço qualquer. N.00','email@email.com','(00) 0-0000-0000','2022-02-28')");

            mb.Sql("INSERT INTO Vendedores(Nome,Cpf,Nascimento,Endereco,Email,Telefone,DataCadastro) " +
                "VALUES('Josenildo Gevildo','380.751.090-74','1978-01-11','R.Endereço qualquer. N.01','email@email.com','(01) 2-3456-7891','2023-03-23')");

            // Clientes
            mb.Sql("INSERT INTO Clientes(Nome,Cpf,Nascimento,Endereco,Email,Telefone,DataCadastro) " +
                "VALUES('Amado Amoroso','026.122.460-37','1978-01-11','R.Endereço qualquer. N.02','email@email.com','(01) 2-3456-7891','2023-08-23')");

            mb.Sql("INSERT INTO Clientes(Nome,Cpf,Nascimento,Endereco,Email,Telefone,DataCadastro) " +
                "VALUES('Chaplin da Silva','218.461.780-61','1999-06-12','R.Endereço qualquer. N.04','email@email.com','(11) 1-1111-1111','2021-01-11')");

            // Ordens
            mb.Sql("INSERT INTO Ordens(VendedorId,ClienteId,Total,StatusVenda,DataCriacao) " +
                "VALUES(1,1,5526.63,3,'2023-06-03')");

            mb.Sql("INSERT INTO Ordens(VendedorId,ClienteId,Total,StatusVenda,DataCriacao) " +
                "VALUES(2,1,164.06,0,'2023-05-09')");

            mb.Sql("INSERT INTO Ordens(VendedorId,ClienteId,Total,StatusVenda,DataCriacao) " +
                "VALUES(2,2,16.35,4,'2023-06-03')");

            // OrdemProduto - Tabela de Junção
            // Ordem - 1
            mb.Sql("INSERT INTO OrdemProdutoJuncao(OrdemId,ProdutoId,NomeProduto,PrecoUnitario,Quantidade,Desconto) " +
                "VALUES(1,9,'Geladeira 110v Branca',999.90,1,99.99)");

            mb.Sql("INSERT INTO OrdemProdutoJuncao(OrdemId,ProdutoId,NomeProduto,PrecoUnitario,Quantidade,Desconto) " +
                "VALUES(1,10,'Batedeira 220v Verm.',259.90,1,25.99)");

            mb.Sql("INSERT INTO OrdemProdutoJuncao(OrdemId,ProdutoId,NomeProduto,PrecoUnitario,Quantidade,Desconto) " +
                "VALUES(1,11,'Maquina de Lavar 110v Branca',1990.00,1,199.00)");

            mb.Sql("INSERT INTO OrdemProdutoJuncao(OrdemId,ProdutoId,NomeProduto,PrecoUnitario,Quantidade,Desconto) " +
                "VALUES(1,12,'Cooktop elétrico 220v',2890.90,1,289.09)");

            // Ordem - 2
            mb.Sql("INSERT INTO OrdemProdutoJuncao(OrdemId,ProdutoId,NomeProduto,PrecoUnitario,Quantidade,Desconto) " +
                "VALUES(2,8,'Pct. c/ 5 meias cores diversas Tam.43',21.90,2,2.19)");

            mb.Sql("INSERT INTO OrdemProdutoJuncao(OrdemId,ProdutoId,NomeProduto,PrecoUnitario,Quantidade,Desconto) " +
                "VALUES(2,6,'Jaqueta Unissex Tam.G',128.90,1,6.45)");

            // Ordem - 3
            mb.Sql("INSERT INTO OrdemProdutoJuncao(OrdemId,ProdutoId,NomeProduto,PrecoUnitario,Quantidade,Desconto) " +
                "VALUES(3,1,'Caderno espiral 100 folhas',7.45,1,0)");

            mb.Sql("INSERT INTO OrdemProdutoJuncao(OrdemId,ProdutoId,NomeProduto,PrecoUnitario,Quantidade,Desconto) " +
                "VALUES(3,2,'Estojo escolar cinza',5.65,1,0)");

            mb.Sql("INSERT INTO OrdemProdutoJuncao(OrdemId,ProdutoId,NomeProduto,PrecoUnitario,Quantidade,Desconto) " +
                "VALUES(3,3,'Borracha branca pequena',3.25,1,0)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Categorias");
            mb.Sql("DELETE FROM Produtos");
            mb.Sql("DELETE FROM Vendedores");
            mb.Sql("DELETE FROM Clientes");
            mb.Sql("DELETE FROM Ordens");
            mb.Sql("DELETE FROM OrdemProdutoJuncao");
        }
    }
}
