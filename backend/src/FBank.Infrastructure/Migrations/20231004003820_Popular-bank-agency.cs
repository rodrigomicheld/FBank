using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Popularbankagency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var idBanco = Guid.NewGuid();
            var idAgencia = Guid.NewGuid();
            var date = DateTime.Now;

            migrationBuilder.Sql($"DELETE FROM Banco WHERE codigo = 1");

            migrationBuilder.Sql($"INSERT INTO Banco (id, codigo, nome, criado_em, atualizado_em) VALUES ('{idBanco}', 1, 'FBank', '{date}', '{date}')");
            migrationBuilder.Sql($"INSERT INTO Agencia (id, codigo, nome, banco_id, criado_em, atualizado_em) VALUES ('{idAgencia}', 1, 'Agencia FBank - 01', '{idBanco}',  '{date}', '{date}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
