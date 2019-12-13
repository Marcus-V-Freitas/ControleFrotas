using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class UpdateAluguel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seguro",
                table: "Alugueis");

            migrationBuilder.AddColumn<int>(
                name: "AluguelSeguroId",
                table: "Alugueis",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_AluguelSeguroId",
                table: "Alugueis",
                column: "AluguelSeguroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alugueis_Seguros_AluguelSeguroId",
                table: "Alugueis",
                column: "AluguelSeguroId",
                principalTable: "Seguros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alugueis_Seguros_AluguelSeguroId",
                table: "Alugueis");

            migrationBuilder.DropIndex(
                name: "IX_Alugueis_AluguelSeguroId",
                table: "Alugueis");

            migrationBuilder.DropColumn(
                name: "AluguelSeguroId",
                table: "Alugueis");

            migrationBuilder.AddColumn<string>(
                name: "Seguro",
                table: "Alugueis",
                nullable: false,
                defaultValue: "");
        }
    }
}
