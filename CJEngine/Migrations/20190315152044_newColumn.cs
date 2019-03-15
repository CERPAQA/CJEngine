using Microsoft.EntityFrameworkCore.Migrations;

namespace CJEngine.Migrations
{
    public partial class newColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairing_Experiment_ExperimentId",
                table: "Pairing");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairing_Judge_JudgeId",
                table: "Pairing");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairing_Artefact_WinnerId",
                table: "Pairing");

            migrationBuilder.DropIndex(
                name: "IX_Pairing_WinnerId",
                table: "Pairing");

            migrationBuilder.DropIndex(
                name: "IX_Experiment_ExperimentParametersId",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Pairing");

            migrationBuilder.AlterColumn<int>(
                name: "JudgeId",
                table: "Pairing",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExperimentId",
                table: "Pairing",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArtefactId",
                table: "Pairing",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pairing_ArtefactId",
                table: "Pairing",
                column: "ArtefactId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiment_ExperimentParametersId",
                table: "Experiment",
                column: "ExperimentParametersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairing_Artefact_ArtefactId",
                table: "Pairing",
                column: "ArtefactId",
                principalTable: "Artefact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairing_Experiment_ExperimentId",
                table: "Pairing",
                column: "ExperimentId",
                principalTable: "Experiment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairing_Judge_JudgeId",
                table: "Pairing",
                column: "JudgeId",
                principalTable: "Judge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairing_Artefact_ArtefactId",
                table: "Pairing");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairing_Experiment_ExperimentId",
                table: "Pairing");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairing_Judge_JudgeId",
                table: "Pairing");

            migrationBuilder.DropIndex(
                name: "IX_Pairing_ArtefactId",
                table: "Pairing");

            migrationBuilder.DropIndex(
                name: "IX_Experiment_ExperimentParametersId",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "ArtefactId",
                table: "Pairing");

            migrationBuilder.AlterColumn<int>(
                name: "JudgeId",
                table: "Pairing",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ExperimentId",
                table: "Pairing",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                table: "Pairing",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pairing_WinnerId",
                table: "Pairing",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiment_ExperimentParametersId",
                table: "Experiment",
                column: "ExperimentParametersId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairing_Experiment_ExperimentId",
                table: "Pairing",
                column: "ExperimentId",
                principalTable: "Experiment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairing_Judge_JudgeId",
                table: "Pairing",
                column: "JudgeId",
                principalTable: "Judge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairing_Artefact_WinnerId",
                table: "Pairing",
                column: "WinnerId",
                principalTable: "Artefact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
