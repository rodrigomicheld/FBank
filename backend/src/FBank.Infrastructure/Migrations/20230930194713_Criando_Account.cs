using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Criando_Account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conta",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cliente_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Agencia_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: false),
                    Saldo = table.Column<decimal>(type: "Decimal(21,9)", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conta", x => x.id);
                    table.ForeignKey(
                        name: "FK_Conta_Agencia_Agencia_id",
                        column: x => x.Agencia_id,
                        principalTable: "Agencia",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conta_Cliente_Cliente_id",
                        column: x => x.Cliente_id,
                        principalTable: "Cliente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conta_Agencia_id",
                table: "Conta",
                column: "Agencia_id");

            migrationBuilder.CreateIndex(
                name: "IX_Conta_Cliente_id",
                table: "Conta",
                column: "Cliente_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conta");
        }
    }
}
