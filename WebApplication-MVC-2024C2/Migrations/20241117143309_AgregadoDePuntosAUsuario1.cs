﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_MVC_2024C2.Migrations
{
    /// <inheritdoc />
    public partial class AgregadoDePuntosAUsuario1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Puntos",
                table: "NuevoUsuario",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Puntos",
                table: "NuevoUsuario");
        }
    }
}