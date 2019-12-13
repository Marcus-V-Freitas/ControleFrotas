using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class EmpresaIdMotorista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Motoristas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motoristas_EmpresaId",
                table: "Motoristas",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Motoristas_ClienteJuridicos_EmpresaId",
                table: "Motoristas",
                column: "EmpresaId",
                principalTable: "ClienteJuridicos",
                principalColumn: "ClienteJuridicoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motoristas_ClienteJuridicos_EmpresaId",
                table: "Motoristas");

            migrationBuilder.DropIndex(
                name: "IX_Motoristas_EmpresaId",
                table: "Motoristas");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Motoristas");
        }
    }
}
