using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnqMeterAPI.Migrations
{
    public partial class DescripcionRespuestaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "DescripcionRespuestas",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   RespuestaId = table.Column<int>(type: "int", nullable: false),
                   Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_DescripcionRespuestas", x => x.Id);
                   table.ForeignKey(
                       name: "FK_DescripcionRespuestas_Respuesta_RespuestaId",
                       column: x => x.RespuestaId,
                       principalTable: "Respuestas",
                       principalColumn: "Id");
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DescripcionRespuestas");
        }
    }
}
