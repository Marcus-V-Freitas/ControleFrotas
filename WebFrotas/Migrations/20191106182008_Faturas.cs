using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class Faturas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaturaAlugueiss_Alugueis_AluguelId",
                table: "FaturaAlugueiss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FaturaAlugueiss",
                table: "FaturaAlugueiss");

            migrationBuilder.RenameTable(
                name: "FaturaAlugueiss",
                newName: "FaturaAlugueis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FaturaAlugueis",
                table: "FaturaAlugueis",
                column: "AluguelId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaturaAlugueis_Alugueis_AluguelId",
                table: "FaturaAlugueis",
                column: "AluguelId",
                principalTable: "Alugueis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaturaAlugueis_Alugueis_AluguelId",
                table: "FaturaAlugueis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FaturaAlugueis",
                table: "FaturaAlugueis");

            migrationBuilder.RenameTable(
                name: "FaturaAlugueis",
                newName: "FaturaAlugueiss");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FaturaAlugueiss",
                table: "FaturaAlugueiss",
                column: "AluguelId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaturaAlugueiss_Alugueis_AluguelId",
                table: "FaturaAlugueiss",
                column: "AluguelId",
                principalTable: "Alugueis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
