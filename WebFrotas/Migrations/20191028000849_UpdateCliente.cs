using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class UpdateCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sexo",
                table: "Clientes",
                newName: "Numero");

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Clientes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "Clientes",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Clientes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Clientes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Clientes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Clientes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "DataPrevista",
                table: "Alugueis",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DadosTransaction",
                table: "Alugueis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormaPagamento",
                table: "Alugueis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Alugueis",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CEP",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "DadosTransaction",
                table: "Alugueis");

            migrationBuilder.DropColumn(
                name: "FormaPagamento",
                table: "Alugueis");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Alugueis");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Clientes",
                newName: "Sexo");

            migrationBuilder.AlterColumn<string>(
                name: "DataPrevista",
                table: "Alugueis",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
