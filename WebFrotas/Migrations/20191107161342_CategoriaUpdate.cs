using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class CategoriaUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "CategoriaVeiculos",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "CategoriaVeiculos");
        }
    }
}
