using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMateInfrastructure.Migrations
{
    public partial class addedIsReasonableToReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReasonable",
                table: "ReportedOffers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Shows if admin finds this report a strike towards the suggester resume");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReasonable",
                table: "ReportedOffers");
        }
    }
}
