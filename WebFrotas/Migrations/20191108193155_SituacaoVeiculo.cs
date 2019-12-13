using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class SituacaoVeiculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "VeiculosEmpresa");

            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "VeiculosCliente");

            migrationBuilder.AddColumn<string>(
                name: "Situacao",
                table: "Veiculos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Veiculos");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "VeiculosEmpresa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Situacao",
                table: "VeiculosCliente",
                nullable: true);
        }
    }
}
