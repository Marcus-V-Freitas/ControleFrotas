using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class VeiculoCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaVeiculoId",
                table: "Veiculos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_CategoriaVeiculoId",
                table: "Veiculos",
                column: "CategoriaVeiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_CategoriaVeiculos_CategoriaVeiculoId",
                table: "Veiculos",
                column: "CategoriaVeiculoId",
                principalTable: "CategoriaVeiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_CategoriaVeiculos_CategoriaVeiculoId",
                table: "Veiculos");

            migrationBuilder.DropIndex(
                name: "IX_Veiculos_CategoriaVeiculoId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "CategoriaVeiculoId",
                table: "Veiculos");
        }
    }
}
