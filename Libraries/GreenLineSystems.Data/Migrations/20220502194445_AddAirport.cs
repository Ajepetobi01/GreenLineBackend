using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenLineSystems.Data.Migrations
{
    public partial class AddAirport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IATA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISOAlpha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}
