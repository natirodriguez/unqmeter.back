using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnqMeterAPI.Migrations
{
    public partial class RespuestaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Respuestas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlydeId = table.Column<int>(type: "int", nullable: false),
                    Participante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    DescripcionGeneral = table.Column<string>(type: "nvarchar(max)", nullable:true),
                    OpcionElegidaId = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuesta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Respuestas_Slydes_SlydeId",
                        column: x => x.SlydeId,
                        principalTable: "Slydes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Respuestas_OpcionesSlydes_OpcionElegidaId",
                        column: x => x.OpcionElegidaId,
                        principalTable: "OpcionesSlydes",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Respuestas");
        }
    }
}
