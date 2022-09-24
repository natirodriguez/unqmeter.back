using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnqMeterAPI.Migrations
{
    public partial class PresentationTableCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Presentaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioCreador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    TiempoDeVida = table.Column<int>(type: "int", nullable: false),
                    TipoTiempoDeVida = table.Column<int>(type: "int", nullable: false),
                    FechaInicioPresentacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaFinPresentacion = table.Column<DateTime>(type: "datetime", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presentaciones", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Presentaciones");
        }
    }
}
