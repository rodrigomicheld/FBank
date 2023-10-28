using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "conta_origem_id",
                table: "Transacao");

            migrationBuilder.RenameColumn(
                name: "flow_type",
                table: "Transacao",
                newName: "fluxo");

            migrationBuilder.AlterColumn<Guid>(
                name: "conta_destino_id",
                table: "Transacao",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fluxo",
                table: "Transacao",
                newName: "flow_type");

            migrationBuilder.AlterColumn<Guid>(
                name: "conta_destino_id",
                table: "Transacao",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "conta_origem_id",
                table: "Transacao",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
