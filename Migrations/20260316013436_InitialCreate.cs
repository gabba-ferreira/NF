using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazaoSocial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TelefoneOp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Pecas",
                columns: table => new
                {
                    IdPeca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomePeca = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CodPeca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QtdEstoque = table.Column<int>(type: "int", nullable: false),
                    Fornecedor = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pecas", x => x.IdPeca);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    IdVeiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PlacaVeiculo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Horimetro = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.IdVeiculo);
                    table.ForeignKey(
                        name: "FK_Veiculos_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdensServico",
                columns: table => new
                {
                    IdOs = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdVeiculo = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Responsavel = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DefeitoDesc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ValorMaoDeObra = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DtVisita = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtFim = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdensServico", x => x.IdOs);
                    table.ForeignKey(
                        name: "FK_OrdensServico_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdensServico_Veiculos_IdVeiculo",
                        column: x => x.IdVeiculo,
                        principalTable: "Veiculos",
                        principalColumn: "IdVeiculo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdemServico_Pecas",
                columns: table => new
                {
                    IdPeca = table.Column<int>(type: "int", nullable: false),
                    IdOs = table.Column<int>(type: "int", nullable: false),
                    QtdPeca = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemServico_Pecas", x => new { x.IdOs, x.IdPeca });
                    table.ForeignKey(
                        name: "FK_OrdemServico_Pecas_OrdensServico_IdOs",
                        column: x => x.IdOs,
                        principalTable: "OrdensServico",
                        principalColumn: "IdOs",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdemServico_Pecas_Pecas_IdPeca",
                        column: x => x.IdPeca,
                        principalTable: "Pecas",
                        principalColumn: "IdPeca",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CNPJ",
                table: "Clientes",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdemServico_Pecas_IdPeca",
                table: "OrdemServico_Pecas",
                column: "IdPeca");

            migrationBuilder.CreateIndex(
                name: "IX_OrdensServico_IdCliente",
                table: "OrdensServico",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_OrdensServico_IdVeiculo",
                table: "OrdensServico",
                column: "IdVeiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_IdCliente",
                table: "Veiculos",
                column: "IdCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdemServico_Pecas");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "OrdensServico");

            migrationBuilder.DropTable(
                name: "Pecas");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
