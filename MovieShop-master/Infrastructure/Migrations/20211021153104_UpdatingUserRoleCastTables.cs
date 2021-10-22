using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdatingUserRoleCastTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "User",
                type: "datetime2",
                maxLength: 7,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "User",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HashedPassword",
                table: "User",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginDateTime",
                table: "User",
                type: "datetime2",
                maxLength: 7,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "User",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LockoutEndDate",
                table: "User",
                type: "datetime2",
                maxLength: 7,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "User",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "User",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TwoFactorEnabled",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isLocked",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Role",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genre",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Cast",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cast",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePath",
                table: "Cast",
                type: "nvarchar(2084)",
                maxLength: 2084,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TmbdUrl",
                table: "Cast",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastLoginDateTime",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LockoutEndDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "User");

            migrationBuilder.DropColumn(
                name: "isLocked",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Cast");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cast");

            migrationBuilder.DropColumn(
                name: "ProfilePath",
                table: "Cast");

            migrationBuilder.DropColumn(
                name: "TmbdUrl",
                table: "Cast");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genre",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);
        }
    }
}
