using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO PRODUTOS(NOME, DESCRICAO, PRECO, " +
                "IMAGEMURL, ESTOQUE, DATACADASTRO, CATEGORIAID) VALUES ('Coca Cola Diet'," +
                " 'Refrigerante de Cola 350 mL', 5.45, 'cocacola.jpg', 50, now(), 1)");

            migrationBuilder.Sql("INSERT INTO PRODUTOS(NOME, DESCRICAO, PRECO, " +
               "IMAGEMURL, ESTOQUE, DATACADASTRO, CATEGORIAID) VALUES ('Lache de Atum'," +
               " 'Lanche de Atum com maionese', 8.50, 'lanchedeatum.jpg', 50, now(), 2)");

            migrationBuilder.Sql("INSERT INTO PRODUTOS(NOME, DESCRICAO, PRECO, " +
               "IMAGEMURL, ESTOQUE, DATACADASTRO, CATEGORIAID) VALUES ('Cocada'," +
               " 'Cocada 250g', 10.0, 'cocada.jpg', 50, now(), 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM PRODUTOS");
        }
    }
}
