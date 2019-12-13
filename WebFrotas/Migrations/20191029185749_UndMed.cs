using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class UndMed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_UnidadeMedida_DespesaMedidaId",
                table: "Despesas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnidadeMedida",
                table: "UnidadeMedida");

            migrationBuilder.RenameTable(
                name: "UnidadeMedida",
                newName: "UnidadeMedidas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnidadeMedidas",
                table: "UnidadeMedidas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_UnidadeMedidas_DespesaMedidaId",
                table: "Despesas",
                column: "DespesaMedidaId",
                principalTable: "UnidadeMedidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_UnidadeMedidas_DespesaMedidaId",
                table: "Despesas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnidadeMedidas",
                table: "UnidadeMedidas");

            migrationBuilder.RenameTable(
                name: "UnidadeMedidas",
                newName: "UnidadeMedida");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnidadeMedida",
                table: "UnidadeMedida",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_UnidadeMedida_DespesaMedidaId",
                table: "Despesas",
                column: "DespesaMedidaId",
                principalTable: "UnidadeMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
