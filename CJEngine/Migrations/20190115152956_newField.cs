using Microsoft.EntityFrameworkCore.Migrations;

namespace CJEngine.Migrations
{
    public partial class newField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Experiment",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Artefact",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Artefact");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Experiment",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
