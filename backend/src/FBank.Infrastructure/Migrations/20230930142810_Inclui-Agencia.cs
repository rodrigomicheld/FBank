using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncluiAgencia : Migration
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
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "codigo_banco",
                table: "Agency",
                newName: "codigo");

            migrationBuilder.AddColumn<Guid>(
                name: "BancoId",
                table: "Agency",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Agency_BancoId",
                table: "Agency",
                column: "BancoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agency_Banco_BancoId",
                table: "Agency",
                column: "BancoId",
                principalTable: "Banco",
                principalColumn: "codigo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agency_Banco_BancoId",
                table: "Agency");

            migrationBuilder.DropIndex(
                name: "IX_Agency_BancoId",
                table: "Agency");

            migrationBuilder.DropColumn(
                name: "BancoId",
                table: "Agency");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Agency",
                newName: "codigo_agencia");

            migrationBuilder.RenameColumn(
                name: "codigo",
                table: "Agency",
                newName: "codigo_banco");

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
