using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Migrations
{
    public partial class NewLoggedTimeFeatureTableAndColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "EstimatedTime",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedDate",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartedDate",
                table: "Jobs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoggedTime",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    LoggedTimeSlice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggedTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoggedTime_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoggedTime_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_AssignedUserId",
                table: "Jobs",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoggedTime_JobId",
                table: "LoggedTime",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_LoggedTime_UserId",
                table: "LoggedTime",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Users_AssignedUserId",
                table: "Jobs",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Users_AssignedUserId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "LoggedTime");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_AssignedUserId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "EstimatedTime",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "FinishedDate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "StartedDate",
                table: "Jobs");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Jobs",
                type: "bigint",
                nullable: true);

        }
    }
}
