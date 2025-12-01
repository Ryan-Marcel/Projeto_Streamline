using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto_Dotnet8.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarUserIdMensagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Mensagens",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Mensagens",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagens_UsuarioId",
                table: "Mensagens",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mensagens_Usuarios_UsuarioId",
                table: "Mensagens",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mensagens_Usuarios_UsuarioId",
                table: "Mensagens");

            migrationBuilder.DropIndex(
                name: "IX_Mensagens_UsuarioId",
                table: "Mensagens");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Mensagens");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Mensagens");
        }
    }
}
