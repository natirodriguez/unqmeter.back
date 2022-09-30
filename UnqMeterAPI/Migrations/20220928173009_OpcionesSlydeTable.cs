using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnqMeterAPI.Migrations
{
    public partial class OpcionesSlydeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpcionesSlydes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlydeId = table.Column<int>(type: "int", nullable: false),
                    Opcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcionesSlydes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcionesSlydes_Slydes_SlydeId",
                        column: x => x.SlydeId,
                        principalTable: "Slydes",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpcionesSlydes");
        }
    }
}
