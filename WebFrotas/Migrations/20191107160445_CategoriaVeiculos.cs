using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class CategoriaVeiculos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_categoriaVeiculos",
                table: "categoriaVeiculos");

            migrationBuilder.RenameTable(
                name: "categoriaVeiculos",
                newName: "CategoriaVeiculos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaVeiculos",
                table: "CategoriaVeiculos",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaVeiculos",
                table: "CategoriaVeiculos");

            migrationBuilder.RenameTable(
                name: "CategoriaVeiculos",
                newName: "categoriaVeiculos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categoriaVeiculos",
                table: "categoriaVeiculos",
                column: "Id");
        }
    }
}
