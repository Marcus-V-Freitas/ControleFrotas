using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class MotoristaAluguel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AluguelMotoristaId",
                table: "Alugueis",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_AluguelMotoristaId",
                table: "Alugueis",
                column: "AluguelMotoristaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alugueis_Motoristas_AluguelMotoristaId",
                table: "Alugueis",
                column: "AluguelMotoristaId",
                principalTable: "Motoristas",
                principalColumn: "ClienteMotoristaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alugueis_Motoristas_AluguelMotoristaId",
                table: "Alugueis");

            migrationBuilder.DropIndex(
                name: "IX_Alugueis_AluguelMotoristaId",
                table: "Alugueis");

            migrationBuilder.DropColumn(
                name: "AluguelMotoristaId",
                table: "Alugueis");
        }
    }
}
