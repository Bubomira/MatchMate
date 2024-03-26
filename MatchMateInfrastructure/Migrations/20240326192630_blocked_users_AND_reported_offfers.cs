using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMateInfrastructure.Migrations
{
    public partial class blocked_users_AND_reported_offfers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Birthday of a user who has to be over 16 years old",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Birthday of a user who has to be over 16 years old");

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "Short information for the user",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Short information for the user");

            migrationBuilder.CreateTable(
                name: "BlockedUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The id of the set, the composite key would have been too large.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockedUserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The id of the blocked user"),
                    BlockerUserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The id of the user who has blocked the other")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockedUsers_AspNetUsers_BlockedUserId",
                        column: x => x.BlockedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlockedUsers_AspNetUsers_BlockerUserId",
                        column: x => x.BlockerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportedOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifyer of the offer report")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReasonForRepport = table.Column<int>(type: "int", nullable: false, comment: "The reason for submitting the report the report"),
                    Comment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Aditional explanations for the offer report"),
                    OfferId = table.Column<int>(type: "int", nullable: false, comment: "The id of the reported offer")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportedOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportedOffers_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUsers_BlockedUserId",
                table: "BlockedUsers",
                column: "BlockedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUsers_BlockerUserId",
                table: "BlockedUsers",
                column: "BlockerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportedOffers_OfferId",
                table: "ReportedOffers",
                column: "OfferId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockedUsers");

            migrationBuilder.DropTable(
                name: "ReportedOffers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                comment: "Birthday of a user who has to be over 16 years old",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Birthday of a user who has to be over 16 years old");

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "Short information for the user",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Short information for the user");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
