using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ajusteModelagemAgencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agency_Banco_codigo_agencia",
                table: "Agency");

            migrationBuilder.RenameColumn(
                name: "codigo_agencia",
                table: "Agency",
                newName: "codigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Agency_Banco_codigo",
                table: "Agency",
                column: "codigo",
                principalTable: "Banco",
                principalColumn: "codigo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agency_Banco_codigo",
                table: "Agency");

            migrationBuilder.RenameColumn(
                name: "codigo",
                table: "Agency",
                newName: "codigo_agencia");

            migrationBuilder.AddForeignKey(
                name: "FK_Agency_Banco_codigo_agencia",
                table: "Agency",
                column: "codigo_agencia",
                principalTable: "Banco",
                principalColumn: "codigo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
