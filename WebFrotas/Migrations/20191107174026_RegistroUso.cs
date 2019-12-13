using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class RegistroUso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistrosUsos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Inicio = table.Column<DateTime>(nullable: false),
                    Prevista = table.Column<DateTime>(nullable: false),
                    Retorno = table.Column<DateTime>(nullable: false),
                    RegistroUsoMotoristaId = table.Column<int>(nullable: false),
                    RegistroVeiculoClienteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosUsos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosUsos_Motoristas_RegistroUsoMotoristaId",
                        column: x => x.RegistroUsoMotoristaId,
                        principalTable: "Motoristas",
                        principalColumn: "ClienteMotoristaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrosUsos_Veiculos_RegistroVeiculoClienteId",
                        column: x => x.RegistroVeiculoClienteId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosUsos_RegistroUsoMotoristaId",
                table: "RegistrosUsos",
                column: "RegistroUsoMotoristaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosUsos_RegistroVeiculoClienteId",
                table: "RegistrosUsos",
                column: "RegistroVeiculoClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosUsos");
        }
    }
}
