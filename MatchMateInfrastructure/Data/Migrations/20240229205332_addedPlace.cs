using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMate.Data.Migrations
{
    public partial class addedPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Offers");

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "Offers",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "",
                comment: "The location with words of the meeting");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Place",
                table: "Offers");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Offers",
                type: "decimal(9,6)",
                nullable: false,
                defaultValue: 0m,
                comment: "Latitude of the offer's location");

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Offers",
                type: "decimal(9,6)",
                nullable: false,
                defaultValue: 0m,
                comment: "Longitude of the offer's location");
        }
    }
}
