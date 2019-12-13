using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class RazaoSocial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeFantasia",
                table: "ClienteJuridicos",
                newName: "RazaoSocial");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RazaoSocial",
                table: "ClienteJuridicos",
                newName: "NomeFantasia");
        }
    }
}
