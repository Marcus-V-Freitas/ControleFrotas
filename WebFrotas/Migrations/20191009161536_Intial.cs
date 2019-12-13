using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebFrotas.Migrations
{
    public partial class Intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(nullable: false),
                    Nascimento = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    CPFCNPJ = table.Column<string>(nullable: false),
                    Tipo = table.Column<string>(nullable: false),
                    Sexo = table.Column<string>(nullable: false),
                    Situacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colaboradores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(nullable: false),
                    Tipo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaboradores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsletterEmails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsletterEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Despesas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    PrecoUnitario = table.Column<double>(nullable: false),
                    UnidadeMedida = table.Column<string>(nullable: false),
                    Quantidade = table.Column<double>(nullable: false),
                    Link = table.Column<string>(nullable: false),
                    DespesaClienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesas_Clientes_DespesaClienteId",
                        column: x => x.DespesaClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    MarcaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modelos_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Placa = table.Column<string>(nullable: false),
                    Renavam = table.Column<string>(nullable: false),
                    Link_Foto = table.Column<string>(nullable: false),
                    ModeloId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculos_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VeiculosCliente",
                columns: table => new
                {
                    VeiculoClienteId = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    ClienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculosCliente", x => x.VeiculoClienteId);
                    table.ForeignKey(
                        name: "FK_VeiculosCliente_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeiculosCliente_Veiculos_VeiculoClienteId",
                        column: x => x.VeiculoClienteId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VeiculosEmpresa",
                columns: table => new
                {
                    VeiculoEmpresaId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Preco_Dia = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculosEmpresa", x => x.VeiculoEmpresaId);
                    table.ForeignKey(
                        name: "FK_VeiculosEmpresa_Veiculos_VeiculoEmpresaId",
                        column: x => x.VeiculoEmpresaId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosDespesas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false),
                    RegistroClienteId = table.Column<int>(nullable: true),
                    RegistroVeiculoId = table.Column<int>(nullable: true),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosDespesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosDespesas_Clientes_RegistroClienteId",
                        column: x => x.RegistroClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrosDespesas_VeiculosCliente_RegistroVeiculoId",
                        column: x => x.RegistroVeiculoId,
                        principalTable: "VeiculosCliente",
                        principalColumn: "VeiculoClienteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alugueis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInicio = table.Column<string>(nullable: true),
                    DataPrevista = table.Column<string>(nullable: true),
                    ValorPrevisto = table.Column<double>(nullable: false),
                    Seguro = table.Column<string>(nullable: false),
                    AluguelVeiculoId = table.Column<int>(nullable: true),
                    AluguelClienteId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alugueis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alugueis_Clientes_AluguelClienteId",
                        column: x => x.AluguelClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alugueis_VeiculosEmpresa_AluguelVeiculoId",
                        column: x => x.AluguelVeiculoId,
                        principalTable: "VeiculosEmpresa",
                        principalColumn: "VeiculoEmpresaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemsRegistros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RegistroId = table.Column<int>(nullable: true),
                    DespesaId = table.Column<int>(nullable: true),
                    QuantidadeItem = table.Column<double>(nullable: false),
                    PrecoUnitario = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsRegistros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsRegistros_Despesas_DespesaId",
                        column: x => x.DespesaId,
                        principalTable: "Despesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsRegistros_RegistrosDespesas_RegistroId",
                        column: x => x.RegistroId,
                        principalTable: "RegistrosDespesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FaturaAlugueiss",
                columns: table => new
                {
                    AluguelId = table.Column<int>(nullable: false),
                    DataInicio = table.Column<string>(nullable: true),
                    DataRetorno = table.Column<string>(nullable: true),
                    ValorTotal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaturaAlugueiss", x => x.AluguelId);
                    table.ForeignKey(
                        name: "FK_FaturaAlugueiss_Alugueis_AluguelId",
                        column: x => x.AluguelId,
                        principalTable: "Alugueis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_AluguelClienteId",
                table: "Alugueis",
                column: "AluguelClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_AluguelVeiculoId",
                table: "Alugueis",
                column: "AluguelVeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_DespesaClienteId",
                table: "Despesas",
                column: "DespesaClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsRegistros_DespesaId",
                table: "ItemsRegistros",
                column: "DespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsRegistros_RegistroId",
                table: "ItemsRegistros",
                column: "RegistroId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_MarcaId",
                table: "Modelos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosDespesas_RegistroClienteId",
                table: "RegistrosDespesas",
                column: "RegistroClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosDespesas_RegistroVeiculoId",
                table: "RegistrosDespesas",
                column: "RegistroVeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_ModeloId",
                table: "Veiculos",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculosCliente_ClienteId",
                table: "VeiculosCliente",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colaboradores");

            migrationBuilder.DropTable(
                name: "FaturaAlugueiss");

            migrationBuilder.DropTable(
                name: "ItemsRegistros");

            migrationBuilder.DropTable(
                name: "NewsletterEmails");

            migrationBuilder.DropTable(
                name: "Alugueis");

            migrationBuilder.DropTable(
                name: "Despesas");

            migrationBuilder.DropTable(
                name: "RegistrosDespesas");

            migrationBuilder.DropTable(
                name: "VeiculosEmpresa");

            migrationBuilder.DropTable(
                name: "VeiculosCliente");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "Marcas");
        }
    }
}
