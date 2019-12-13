using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class updateRegistro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrosUsos_Veiculos_RegistroVeiculoClienteId",
                table: "RegistrosUsos");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrosUsos_VeiculosCliente_RegistroVeiculoClienteId",
                table: "RegistrosUsos",
                column: "RegistroVeiculoClienteId",
                principalTable: "VeiculosCliente",
                principalColumn: "VeiculoClienteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrosUsos_VeiculosCliente_RegistroVeiculoClienteId",
                table: "RegistrosUsos");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrosUsos_Veiculos_RegistroVeiculoClienteId",
                table: "RegistrosUsos",
                column: "RegistroVeiculoClienteId",
                principalTable: "Veiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
