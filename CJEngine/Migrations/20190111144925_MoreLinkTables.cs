using Microsoft.EntityFrameworkCore.Migrations;

namespace CJEngine.Migrations
{
    public partial class MoreLinkTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiment_ExperimentParameters_ParametersId",
                table: "Experiment");

            migrationBuilder.DropIndex(
                name: "IX_Experiment_ParametersId",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "Artefact1",
                table: "Pairing");

            migrationBuilder.DropColumn(
                name: "Artefact2",
                table: "Pairing");

            migrationBuilder.DropColumn(
                name: "Winner",
                table: "Pairing");

            migrationBuilder.DropColumn(
                name: "ParametersId",
                table: "Experiment");

            migrationBuilder.AddColumn<int>(
                name: "JudgeId",
                table: "Pairing",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                table: "Pairing",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExperimentParametersId",
                table: "Experiment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ArtefactPairings",
                columns: table => new
                {
                    ArtefactId = table.Column<int>(nullable: false),
                    PairingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtefactPairings", x => new { x.ArtefactId, x.PairingId });
                    table.ForeignKey(
                        name: "FK_ArtefactPairings_Artefact_ArtefactId",
                        column: x => x.ArtefactId,
                        principalTable: "Artefact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtefactPairings_Pairing_PairingId",
                        column: x => x.PairingId,
                        principalTable: "Pairing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pairing_JudgeId",
                table: "Pairing",
                column: "JudgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pairing_WinnerId",
                table: "Pairing",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiment_ExperimentParametersId",
                table: "Experiment",
                column: "ExperimentParametersId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArtefactPairings_PairingId",
                table: "ArtefactPairings",
                column: "PairingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiment_ExperimentParameters_ExperimentParametersId",
                table: "Experiment",
                column: "ExperimentParametersId",
                principalTable: "ExperimentParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiment_ExperimentParameters_ExperimentParametersId",
                table: "Experiment");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairing_Judge_JudgeId",
                table: "Pairing");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairing_Artefact_WinnerId",
                table: "Pairing");

            migrationBuilder.DropTable(
                name: "ArtefactPairings");

            migrationBuilder.DropIndex(
                name: "IX_Pairing_JudgeId",
                table: "Pairing");

            migrationBuilder.DropIndex(
                name: "IX_Pairing_WinnerId",
                table: "Pairing");

            migrationBuilder.DropIndex(
                name: "IX_Experiment_ExperimentParametersId",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "JudgeId",
                table: "Pairing");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Pairing");

            migrationBuilder.DropColumn(
                name: "ExperimentParametersId",
                table: "Experiment");

            migrationBuilder.AddColumn<int>(
                name: "Artefact1",
                table: "Pairing",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Artefact2",
                table: "Pairing",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Winner",
                table: "Pairing",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParametersId",
                table: "Experiment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experiment_ParametersId",
                table: "Experiment",
                column: "ParametersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiment_ExperimentParameters_ParametersId",
                table: "Experiment",
                column: "ParametersId",
                principalTable: "ExperimentParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
