using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncluirAgencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codigo_banco",
                table: "Banco");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Banco",
                newName: "codigo");

            migrationBuilder.CreateTable(
                name: "Agency",
                columns: table => new
                {
                    codigo_agencia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo_banco = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agency", x => x.codigo_agencia);
                    table.ForeignKey(
                        name: "FK_Agency_Banco_codigo_agencia",
                        column: x => x.codigo_agencia,
                        principalTable: "Banco",
                        principalColumn: "codigo",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agency");

            migrationBuilder.RenameColumn(
                name: "codigo",
                table: "Banco",
                newName: "id");

            migrationBuilder.AddColumn<int>(
                name: "codigo_banco",
                table: "Banco",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
