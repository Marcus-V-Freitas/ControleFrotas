using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class RemoveCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrosDespesas_Clientes_RegistroClienteId",
                table: "RegistrosDespesas");

            migrationBuilder.DropIndex(
                name: "IX_RegistrosDespesas_RegistroClienteId",
                table: "RegistrosDespesas");

            migrationBuilder.DropColumn(
                name: "RegistroClienteId",
                table: "RegistrosDespesas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegistroClienteId",
                table: "RegistrosDespesas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosDespesas_RegistroClienteId",
                table: "RegistrosDespesas",
                column: "RegistroClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrosDespesas_Clientes_RegistroClienteId",
                table: "RegistrosDespesas",
                column: "RegistroClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
