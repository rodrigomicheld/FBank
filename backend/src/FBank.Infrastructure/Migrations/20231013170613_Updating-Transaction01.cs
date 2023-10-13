using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingTransaction01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_Conta_conta_destino_id",
                table: "Transacao");

            migrationBuilder.DropIndex(
                name: "IX_Transacao_conta_destino_id",
                table: "Transacao");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Transacao",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_AccountId",
                table: "Transacao",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_Conta_AccountId",
                table: "Transacao",
                column: "AccountId",
                principalTable: "Conta",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_Conta_AccountId",
                table: "Transacao");

            migrationBuilder.DropIndex(
                name: "IX_Transacao_AccountId",
                table: "Transacao");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Transacao");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_conta_destino_id",
                table: "Transacao",
                column: "conta_destino_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_Conta_conta_destino_id",
                table: "Transacao",
                column: "conta_destino_id",
                principalTable: "Conta",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
