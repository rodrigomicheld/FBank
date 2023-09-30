using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncluirAgency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Banco_codigo_banco",
                table: "Banco");

            migrationBuilder.RenameColumn(
                name: "codigo_banco",
                table: "Banco",
                newName: "codigo");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Banco_codigo",
                table: "Banco",
                column: "codigo");

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

            migrationBuilder.CreateIndex(
                name: "IX_Agencia_banco_id",
                table: "Agencia",
                column: "banco_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agencia");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Banco_codigo",
                table: "Banco");

            migrationBuilder.RenameColumn(
                name: "codigo",
                table: "Banco",
                newName: "codigo_banco");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Banco_codigo_banco",
                table: "Banco",
                column: "codigo_banco");
        }
    }
}
