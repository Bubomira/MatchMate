using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMateInfrastructure.Migrations
{
    public partial class datesendmessagecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateSend",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "When the message is sent, used for sorting the chat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSend",
                table: "Messages");
        }
    }
}
