using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class UpdateClienteAluguel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alugueis_Clientes_AluguelClienteId",
                table: "Alugueis");

            migrationBuilder.AddForeignKey(
                name: "FK_Alugueis_ClienteJuridicos_AluguelClienteId",
                table: "Alugueis",
                column: "AluguelClienteId",
                principalTable: "ClienteJuridicos",
                principalColumn: "ClienteJuridicoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alugueis_ClienteJuridicos_AluguelClienteId",
                table: "Alugueis");

            migrationBuilder.AddForeignKey(
                name: "FK_Alugueis_Clientes_AluguelClienteId",
                table: "Alugueis",
                column: "AluguelClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
