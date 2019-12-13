using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class UnidadeMedida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnidadeMedida",
                table: "Despesas");

            migrationBuilder.AddColumn<int>(
                name: "DespesaMedidaId",
                table: "Despesas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UnidadeMedida",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeMedida", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_DespesaMedidaId",
                table: "Despesas",
                column: "DespesaMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_UnidadeMedida_DespesaMedidaId",
                table: "Despesas",
                column: "DespesaMedidaId",
                principalTable: "UnidadeMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_UnidadeMedida_DespesaMedidaId",
                table: "Despesas");

            migrationBuilder.DropTable(
                name: "UnidadeMedida");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_DespesaMedidaId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "DespesaMedidaId",
                table: "Despesas");

            migrationBuilder.AddColumn<string>(
                name: "UnidadeMedida",
                table: "Despesas",
                nullable: false,
                defaultValue: "");
        }
    }
}
