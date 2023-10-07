using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContaDestino : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Agencia_Agencia_id",
                table: "Conta");

            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Cliente_Cliente_id",
                table: "Conta");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_Conta_conta_id",
                table: "Transacao");

            migrationBuilder.DropIndex(
                name: "IX_Transacao_conta_id",
                table: "Transacao");

            migrationBuilder.RenameColumn(
                name: "conta_id",
                table: "Transacao",
                newName: "conta_origem_id");

            migrationBuilder.RenameColumn(
                name: "Status_Id",
                table: "Conta",
                newName: "status_Id");

            migrationBuilder.RenameColumn(
                name: "Cliente_id",
                table: "Conta",
                newName: "cliente_id");

            migrationBuilder.RenameColumn(
                name: "Agencia_id",
                table: "Conta",
                newName: "agencia_id");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Conta",
                newName: "saldo");

            migrationBuilder.RenameIndex(
                name: "IX_Conta_Cliente_id",
                table: "Conta",
                newName: "IX_Conta_cliente_id");

            migrationBuilder.RenameIndex(
                name: "IX_Conta_Agencia_id",
                table: "Conta",
                newName: "IX_Conta_agencia_id");

            migrationBuilder.AddColumn<int>(
                name: "FlowType",
                table: "Transacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "conta_destino_id",
                table: "Transacao",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "saldo",
                table: "Conta",
                type: "Decimal(21,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(21,9)");

            migrationBuilder.AddColumn<int>(
                name: "numero",
                table: "Conta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_conta_destino_id",
                table: "Transacao",
                column: "conta_destino_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Agencia_agencia_id",
                table: "Conta",
                column: "agencia_id",
                principalTable: "Agencia",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Cliente_cliente_id",
                table: "Conta",
                column: "cliente_id",
                principalTable: "Cliente",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_Conta_conta_destino_id",
                table: "Transacao",
                column: "conta_destino_id",
                principalTable: "Conta",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Agencia_agencia_id",
                table: "Conta");

            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Cliente_cliente_id",
                table: "Conta");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_Conta_conta_destino_id",
                table: "Transacao");

            migrationBuilder.DropIndex(
                name: "IX_Transacao_conta_destino_id",
                table: "Transacao");

            migrationBuilder.DropColumn(
                name: "FlowType",
                table: "Transacao");

            migrationBuilder.DropColumn(
                name: "conta_destino_id",
                table: "Transacao");

            migrationBuilder.DropColumn(
                name: "numero",
                table: "Conta");

            migrationBuilder.RenameColumn(
                name: "conta_origem_id",
                table: "Transacao",
                newName: "conta_id");

            migrationBuilder.RenameColumn(
                name: "status_Id",
                table: "Conta",
                newName: "Status_Id");

            migrationBuilder.RenameColumn(
                name: "cliente_id",
                table: "Conta",
                newName: "Cliente_id");

            migrationBuilder.RenameColumn(
                name: "agencia_id",
                table: "Conta",
                newName: "Agencia_id");

            migrationBuilder.RenameColumn(
                name: "saldo",
                table: "Conta",
                newName: "Balance");

            migrationBuilder.RenameIndex(
                name: "IX_Conta_cliente_id",
                table: "Conta",
                newName: "IX_Conta_Cliente_id");

            migrationBuilder.RenameIndex(
                name: "IX_Conta_agencia_id",
                table: "Conta",
                newName: "IX_Conta_Agencia_id");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "Conta",
                type: "Decimal(21,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(21,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_conta_id",
                table: "Transacao",
                column: "conta_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Agencia_Agencia_id",
                table: "Conta",
                column: "Agencia_id",
                principalTable: "Agencia",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Cliente_Cliente_id",
                table: "Conta",
                column: "Cliente_id",
                principalTable: "Cliente",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_Conta_conta_id",
                table: "Transacao",
                column: "conta_id",
                principalTable: "Conta",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
