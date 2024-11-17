using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_MVC_2024C2.Migrations
{
    /// <inheritdoc />
    public partial class AgregadoDePuntosAUsuario5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contraseña",
                table: "NuevoUsuario",
                newName: "Contrasenia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contrasenia",
                table: "NuevoUsuario",
                newName: "Contraseña");
        }
    }
}
