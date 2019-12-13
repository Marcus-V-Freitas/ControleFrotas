using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class MotoristaRegistro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegistroMotoristaId",
                table: "RegistrosDespesas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosDespesas_RegistroMotoristaId",
                table: "RegistrosDespesas",
                column: "RegistroMotoristaId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrosDespesas_Motoristas_RegistroMotoristaId",
                table: "RegistrosDespesas",
                column: "RegistroMotoristaId",
                principalTable: "Motoristas",
                principalColumn: "ClienteMotoristaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrosDespesas_Motoristas_RegistroMotoristaId",
                table: "RegistrosDespesas");

            migrationBuilder.DropIndex(
                name: "IX_RegistrosDespesas_RegistroMotoristaId",
                table: "RegistrosDespesas");

            migrationBuilder.DropColumn(
                name: "RegistroMotoristaId",
                table: "RegistrosDespesas");
        }
    }
}
