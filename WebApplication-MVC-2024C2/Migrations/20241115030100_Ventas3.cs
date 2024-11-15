using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_MVC_2024C2.Migrations
{
    /// <inheritdoc />
    public partial class Ventas3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeliculaId",
                table: "Ventas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_PeliculaId",
                table: "Ventas",
                column: "PeliculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Peliculas_PeliculaId",
                table: "Ventas",
                column: "PeliculaId",
                principalTable: "Peliculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Peliculas_PeliculaId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_PeliculaId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "PeliculaId",
                table: "Ventas");
        }
    }
}
