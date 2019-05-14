using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CJEngine.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Researcher",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Researcher",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Researcher",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Researcher",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Researcher",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Researcher",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Researcher",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Researcher",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Researcher",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Researcher",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Researcher",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Researcher",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Researcher",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Researcher",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Judge",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Judge",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Judge",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Judge",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Judge",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Judge",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Judge",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Judge",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Judge",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Judge",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Judge",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Judge",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Judge",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Researcher");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Researcher");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Researcher");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Researcher");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Researcher");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Researcher");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Researcher");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Researcher");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Researcher");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Researcher");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Researcher");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Researcher");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Researcher");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Researcher");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Judge");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Judge");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Judge");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Judge");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Judge");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Judge");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Judge");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Judge");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Judge");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Judge");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Judge");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Judge");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Judge");
        }
    }
}
