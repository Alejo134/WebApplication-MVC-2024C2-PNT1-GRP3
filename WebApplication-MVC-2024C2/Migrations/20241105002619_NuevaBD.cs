using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_MVC_2024C2.Migrations
{
    /// <inheritdoc />
    public partial class NuevaBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Butacas");

            migrationBuilder.AddColumn<int>(
                name: "CantButacas",
                table: "Peliculas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPelicula = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantButacas = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropColumn(
                name: "CantButacas",
                table: "Peliculas");

            migrationBuilder.CreateTable(
                name: "Butacas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Disponible = table.Column<bool>(type: "bit", nullable: false),
                    PeliculaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Butacas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Butacas_Peliculas_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Butacas_PeliculaId",
                table: "Butacas",
                column: "PeliculaId");
        }
    }
}
