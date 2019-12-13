using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class Clientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClienteFisicos",
                columns: table => new
                {
                    ClienteFisicoId = table.Column<int>(nullable: false),
                    Sexo = table.Column<string>(nullable: false),
                    RG = table.Column<string>(nullable: false),
                    Renach = table.Column<string>(nullable: false),
                    CNH = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteFisicos", x => x.ClienteFisicoId);
                    table.ForeignKey(
                        name: "FK_ClienteFisicos_Clientes_ClienteFisicoId",
                        column: x => x.ClienteFisicoId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClienteJuridicos",
                columns: table => new
                {
                    ClienteJuridicoId = table.Column<int>(nullable: false),
                    NomeFantasia = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteJuridicos", x => x.ClienteJuridicoId);
                    table.ForeignKey(
                        name: "FK_ClienteJuridicos_Clientes_ClienteJuridicoId",
                        column: x => x.ClienteJuridicoId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Motoristas",
                columns: table => new
                {
                    ClienteMotoristaId = table.Column<int>(nullable: false),
                    Sexo = table.Column<string>(nullable: false),
                    Nascimento = table.Column<string>(nullable: false),
                    RG = table.Column<string>(nullable: false),
                    Renach = table.Column<string>(nullable: false),
                    CNH = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motoristas", x => x.ClienteMotoristaId);
                    table.ForeignKey(
                        name: "FK_Motoristas_Clientes_ClienteMotoristaId",
                        column: x => x.ClienteMotoristaId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteFisicos");

            migrationBuilder.DropTable(
                name: "ClienteJuridicos");

            migrationBuilder.DropTable(
                name: "Motoristas");
        }
    }
}
