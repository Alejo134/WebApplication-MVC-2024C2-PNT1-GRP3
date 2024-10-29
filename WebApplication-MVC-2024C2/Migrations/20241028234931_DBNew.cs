using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_MVC_2024C2.Migrations
{
    /// <inheritdoc />
    public partial class DBNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Butacas_Peliculas_PeliculaId",
                table: "Butacas");

            migrationBuilder.DropIndex(
                name: "IX_Butacas_PeliculaId",
                table: "Butacas");

            migrationBuilder.DropColumn(
                name: "PeliculaId",
                table: "Butacas");

            migrationBuilder.AddColumn<int>(
                name: "ButacasId",
                table: "Peliculas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPelicula",
                table: "Butacas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_ButacasId",
                table: "Peliculas",
                column: "ButacasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_Butacas_ButacasId",
                table: "Peliculas",
                column: "ButacasId",
                principalTable: "Butacas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_Butacas_ButacasId",
                table: "Peliculas");

            migrationBuilder.DropIndex(
                name: "IX_Peliculas_ButacasId",
                table: "Peliculas");

            migrationBuilder.DropColumn(
                name: "ButacasId",
                table: "Peliculas");

            migrationBuilder.DropColumn(
                name: "IdPelicula",
                table: "Butacas");

            migrationBuilder.AddColumn<int>(
                name: "PeliculaId",
                table: "Butacas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Butacas_PeliculaId",
                table: "Butacas",
                column: "PeliculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Butacas_Peliculas_PeliculaId",
                table: "Butacas",
                column: "PeliculaId",
                principalTable: "Peliculas",
                principalColumn: "Id");
        }
    }
}
