using Microsoft.EntityFrameworkCore.Migrations;

namespace CJEngine.Migrations
{
    public partial class numPairings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfPairings",
                table: "ExperimentParameters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfPairings",
                table: "ExperimentParameters");
        }
    }
}
