﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnqMeterAPI.Migrations
{
    public partial class SlydeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slydes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreguntaRealizada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoPregunta = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    PresentacionID = table.Column<int>(nullable: false),
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slydes");
        }
    }
}
