using Microsoft.EntityFrameworkCore.Migrations;

namespace CJEngine.Migrations
{
    public partial class AllTableLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExperimentId",
                table: "Pairing",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pairing_ExperimentId",
                table: "Pairing",
                column: "ExperimentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairing_Experiment_ExperimentId",
                table: "Pairing",
                column: "ExperimentId",
                principalTable: "Experiment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairing_Experiment_ExperimentId",
                table: "Pairing");

            migrationBuilder.DropIndex(
                name: "IX_Pairing_ExperimentId",
                table: "Pairing");

            migrationBuilder.DropColumn(
                name: "ExperimentId",
                table: "Pairing");
        }
    }
}
