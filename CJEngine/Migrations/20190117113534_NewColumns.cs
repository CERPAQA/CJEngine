using Microsoft.EntityFrameworkCore.Migrations;

namespace CJEngine.Migrations
{
    public partial class NewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ExperimentParameters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Experiment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ExperimentParameters");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Experiment");
        }
    }
}
