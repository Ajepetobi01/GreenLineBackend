using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenLineSystems.Data.Migrations
{
    public partial class AddDetailstoPassenger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "PassengerDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "IllegalImmigration",
                table: "PassengerDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Narcotics",
                table: "PassengerDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "PassengerDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "PassengerDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Revenue",
                table: "PassengerDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Smuggling",
                table: "PassengerDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Terrorism",
                table: "PassengerDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "PassengerDetails");

            migrationBuilder.DropColumn(
                name: "IllegalImmigration",
                table: "PassengerDetails");

            migrationBuilder.DropColumn(
                name: "Narcotics",
                table: "PassengerDetails");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "PassengerDetails");

            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "PassengerDetails");

            migrationBuilder.DropColumn(
                name: "Revenue",
                table: "PassengerDetails");

            migrationBuilder.DropColumn(
                name: "Smuggling",
                table: "PassengerDetails");

            migrationBuilder.DropColumn(
                name: "Terrorism",
                table: "PassengerDetails");
        }
    }
}
