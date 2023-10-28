using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banco",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banco", x => x.id);
                    table.UniqueConstraint("AK_Banco_codigo", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    documento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    senha = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tipo_documento = table.Column<int>(type: "int", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.id);
                    table.UniqueConstraint("AK_Cliente_documento", x => x.documento);
                });

            migrationBuilder.CreateTable(
                name: "Agencia",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    banco_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencia", x => x.id);
                    table.ForeignKey(
                        name: "FK_Agencia_Banco_banco_id",
                        column: x => x.banco_id,
                        principalTable: "Banco",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conta",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cliente_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    agencia_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    saldo = table.Column<decimal>(type: "Decimal(21,2)", nullable: false),
                    numero = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conta", x => x.id);
                    table.UniqueConstraint("AK_Conta_numero", x => x.numero);
                    table.ForeignKey(
                        name: "FK_Conta_Agencia_agencia_id",
                        column: x => x.agencia_id,
                        principalTable: "Agencia",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conta_Cliente_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "Cliente",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Transacao",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipo_transacao = table.Column<int>(type: "int", nullable: false),
                    valor = table.Column<decimal>(type: "Decimal(21,2)", nullable: false),
                    conta_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    conta_destino_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    fluxo = table.Column<int>(type: "int", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacao", x => x.id);
                    table.ForeignKey(
                        name: "FK_Transacao_Conta_conta_id",
                        column: x => x.conta_id,
                        principalTable: "Conta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agencia_banco_id",
                table: "Agencia",
                column: "banco_id");

            migrationBuilder.CreateIndex(
                name: "IX_Conta_agencia_id",
                table: "Conta",
                column: "agencia_id");

            migrationBuilder.CreateIndex(
                name: "IX_Conta_cliente_id",
                table: "Conta",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_conta_id",
                table: "Transacao",
                column: "conta_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacao");

            migrationBuilder.DropTable(
                name: "Conta");

            migrationBuilder.DropTable(
                name: "Agencia");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Banco");
        }
    }
}
