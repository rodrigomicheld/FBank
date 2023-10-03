using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AccountAdjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Cliente_Cliente_id",
                table: "Conta");

            migrationBuilder.DropIndex(
                name: "IX_Conta_Cliente_id",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "conta_id",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "IdStatus",
                table: "Conta",
                newName: "Status_Id");

            migrationBuilder.RenameColumn(
                name: "Saldo",
                table: "Conta",
                newName: "Balance");

            migrationBuilder.CreateIndex(
                name: "IX_Conta_Cliente_id",
                table: "Conta",
                column: "Cliente_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Cliente_Cliente_id",
                table: "Conta",
                column: "Cliente_id",
                principalTable: "Cliente",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Cliente_Cliente_id",
                table: "Conta");

            migrationBuilder.DropIndex(
                name: "IX_Conta_Cliente_id",
                table: "Conta");

            migrationBuilder.RenameColumn(
                name: "Status_Id",
                table: "Conta",
                newName: "IdStatus");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Conta",
                newName: "Saldo");

            migrationBuilder.AddColumn<Guid>(
                name: "conta_id",
                table: "Cliente",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Conta_Cliente_id",
                table: "Conta",
                column: "Cliente_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Cliente_Cliente_id",
                table: "Conta",
                column: "Cliente_id",
                principalTable: "Cliente",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
