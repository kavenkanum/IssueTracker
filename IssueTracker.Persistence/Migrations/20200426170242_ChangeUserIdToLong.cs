using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Migrations
{
    public partial class ChangeUserIdToLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex("IX_Jobs_UserId", "Jobs");
            migrationBuilder.DropColumn("Id", "Users");
            migrationBuilder.DropColumn("UserId", "Jobs");
            migrationBuilder.DropColumn("AssignedUserId", "Jobs");
            migrationBuilder.DropColumn("CreatorId", "Jobs");
            migrationBuilder.DropColumn("UserId", "Comment");


            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Users",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorId",
                table: "Jobs",
                nullable: false);

            migrationBuilder.AddColumn<long>(
                name: "AssignedUserId",
                table: "Jobs",
                nullable: false);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Comment",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey("Id", "Users");
            migrationBuilder.DropColumn("Id", "Users");
            migrationBuilder.DropColumn("UserId", "Jobs");
            migrationBuilder.DropColumn("AssignedUserId", "Jobs");
            migrationBuilder.DropColumn("CreatorId", "Jobs");
            migrationBuilder.DropColumn("UserId", "Comment");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedUserId",
                table: "Jobs",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Comment",
                type: "uniqueidentifier",
                nullable: false);
        }
    }
}
