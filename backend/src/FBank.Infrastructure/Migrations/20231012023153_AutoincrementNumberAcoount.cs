using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AutoincrementNumberAcoount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numero",
                table: "Conta");

            migrationBuilder.AddColumn<int>(
                name: "numero",
                table: "Conta",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
