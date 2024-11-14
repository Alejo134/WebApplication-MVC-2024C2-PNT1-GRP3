using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_MVC_2024C2.Migrations
{
    /// <inheritdoc />
    public partial class nuevoUsuario2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NuevoUsuario_1",
                table: "NuevoUsuario_1");

            migrationBuilder.RenameTable(
                name: "NuevoUsuario_1",
                newName: "NuevoUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NuevoUsuario",
                table: "NuevoUsuario",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NuevoUsuario",
                table: "NuevoUsuario");

            migrationBuilder.RenameTable(
                name: "NuevoUsuario",
                newName: "NuevoUsuario_1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NuevoUsuario_1",
                table: "NuevoUsuario_1",
                column: "Id");
        }
    }
}
