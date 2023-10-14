using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingTransaction02withAccountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_Conta_AccountId",
                table: "Transacao");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Transacao",
                newName: "conta_id");

            migrationBuilder.RenameIndex(
                name: "IX_Transacao_AccountId",
                table: "Transacao",
                newName: "IX_Transacao_conta_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_Conta_conta_id",
                table: "Transacao",
                column: "conta_id",
                principalTable: "Conta",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_Conta_conta_id",
                table: "Transacao");

            migrationBuilder.RenameColumn(
                name: "conta_id",
                table: "Transacao",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacao_conta_id",
                table: "Transacao",
                newName: "IX_Transacao_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_Conta_AccountId",
                table: "Transacao",
                column: "AccountId",
                principalTable: "Conta",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
