using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CJEngine.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Algorithm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FunctionName = table.Column<string>(nullable: true),
                    Filename = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Valid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Algorithm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artefact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artefact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExperimentParameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    ShowTitle = table.Column<bool>(nullable: false),
                    ShowTimer = table.Column<bool>(nullable: false),
                    AddComment = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Judge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Judge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Researcher",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Researcher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ExperimentParametersId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiment_ExperimentParameters_ExperimentParametersId",
                        column: x => x.ExperimentParametersId,
                        principalTable: "ExperimentParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpAlgorithm",
                columns: table => new
                {
                    ExperimentId = table.Column<int>(nullable: false),
                    AlgorithmId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpAlgorithm", x => new { x.ExperimentId, x.AlgorithmId });
                    table.ForeignKey(
                        name: "FK_ExpAlgorithm_Algorithm_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "Algorithm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpAlgorithm_Experiment_ExperimentId",
                        column: x => x.ExperimentId,
                        principalTable: "Experiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpArtefact",
                columns: table => new
                {
                    ExperimentId = table.Column<int>(nullable: false),
                    ArtefactId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpArtefact", x => new { x.ExperimentId, x.ArtefactId });
                    table.ForeignKey(
                        name: "FK_ExpArtefact_Artefact_ArtefactId",
                        column: x => x.ArtefactId,
                        principalTable: "Artefact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpArtefact_Experiment_ExperimentId",
                        column: x => x.ExperimentId,
                        principalTable: "Experiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpJudge",
                columns: table => new
                {
                    ExperimentId = table.Column<int>(nullable: false),
                    JudgeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpJudge", x => new { x.ExperimentId, x.JudgeId });
                    table.ForeignKey(
                        name: "FK_ExpJudge_Experiment_ExperimentId",
                        column: x => x.ExperimentId,
                        principalTable: "Experiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpJudge_Judge_JudgeId",
                        column: x => x.JudgeId,
                        principalTable: "Judge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpResearcher",
                columns: table => new
                {
                    ExperimentId = table.Column<int>(nullable: false),
                    ResearcherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpResearcher", x => new { x.ExperimentId, x.ResearcherId });
                    table.ForeignKey(
                        name: "FK_ExpResearcher_Experiment_ExperimentId",
                        column: x => x.ExperimentId,
                        principalTable: "Experiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpResearcher_Researcher_ResearcherId",
                        column: x => x.ResearcherId,
                        principalTable: "Researcher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pairing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JudgeId = table.Column<int>(nullable: false),
                    ExperimentId = table.Column<int>(nullable: false),
                    WinnerId = table.Column<int>(nullable: false),
                    TimeOfPairing = table.Column<DateTime>(nullable: false),
                    ElapsedTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pairing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pairing_Experiment_ExperimentId",
                        column: x => x.ExperimentId,
                        principalTable: "Experiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pairing_Judge_JudgeId",
                        column: x => x.JudgeId,
                        principalTable: "Judge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pairing_Artefact_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Artefact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_ArtefactPairings_PairingId",
                table: "ArtefactPairings",
                column: "PairingId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpAlgorithm_AlgorithmId",
                table: "ExpAlgorithm",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpArtefact_ArtefactId",
                table: "ExpArtefact",
                column: "ArtefactId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiment_ExperimentParametersId",
                table: "Experiment",
                column: "ExperimentParametersId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpJudge_JudgeId",
                table: "ExpJudge",
                column: "JudgeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpResearcher_ResearcherId",
                table: "ExpResearcher",
                column: "ResearcherId");

            migrationBuilder.CreateIndex(
                name: "IX_Pairing_ExperimentId",
                table: "Pairing",
                column: "ExperimentId");

            migrationBuilder.CreateIndex(
                name: "IX_Pairing_JudgeId",
                table: "Pairing",
                column: "JudgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pairing_WinnerId",
                table: "Pairing",
                column: "WinnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtefactPairings");

            migrationBuilder.DropTable(
                name: "ExpAlgorithm");

            migrationBuilder.DropTable(
                name: "ExpArtefact");

            migrationBuilder.DropTable(
                name: "ExpJudge");

            migrationBuilder.DropTable(
                name: "ExpResearcher");

            migrationBuilder.DropTable(
                name: "Pairing");

            migrationBuilder.DropTable(
                name: "Algorithm");

            migrationBuilder.DropTable(
                name: "Researcher");

            migrationBuilder.DropTable(
                name: "Experiment");

            migrationBuilder.DropTable(
                name: "Judge");

            migrationBuilder.DropTable(
                name: "Artefact");

            migrationBuilder.DropTable(
                name: "ExperimentParameters");
        }
    }
}
