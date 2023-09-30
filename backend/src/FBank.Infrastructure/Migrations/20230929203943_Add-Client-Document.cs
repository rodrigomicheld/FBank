using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClientDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "documento",
                table: "Cliente",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "tipo_documento",
                table: "Cliente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Cliente_documento",
                table: "Cliente",
                column: "documento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Cliente_documento",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "documento",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "tipo_documento",
                table: "Cliente");
        }
    }
}
