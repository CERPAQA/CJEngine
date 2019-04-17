using Microsoft.EntityFrameworkCore.Migrations;

namespace CJEngine.Migrations
{
    public partial class ColumnChangesUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpJudge_Judge_JudgeId",
                table: "ExpJudge");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpResearcher_Researcher_ResearcherId",
                table: "ExpResearcher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpResearcher",
                table: "ExpResearcher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpJudge",
                table: "ExpJudge");

            migrationBuilder.AlterColumn<int>(
                name: "ResearcherId",
                table: "ExpResearcher",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "ResearcherLoginId",
                table: "ExpResearcher",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "JudgeId",
                table: "ExpJudge",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "JudgeLoginId",
                table: "ExpJudge",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpResearcher",
                table: "ExpResearcher",
                columns: new[] { "ExperimentId", "ResearcherLoginId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpJudge",
                table: "ExpJudge",
                columns: new[] { "ExperimentId", "JudgeLoginId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ExpJudge_Judge_JudgeId",
                table: "ExpJudge",
                column: "JudgeId",
                principalTable: "Judge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpResearcher_Researcher_ResearcherId",
                table: "ExpResearcher",
                column: "ResearcherId",
                principalTable: "Researcher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpJudge_Judge_JudgeId",
                table: "ExpJudge");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpResearcher_Researcher_ResearcherId",
                table: "ExpResearcher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpResearcher",
                table: "ExpResearcher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpJudge",
                table: "ExpJudge");

            migrationBuilder.DropColumn(
                name: "ResearcherLoginId",
                table: "ExpResearcher");

            migrationBuilder.DropColumn(
                name: "JudgeLoginId",
                table: "ExpJudge");

            migrationBuilder.AlterColumn<int>(
                name: "ResearcherId",
                table: "ExpResearcher",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JudgeId",
                table: "ExpJudge",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpResearcher",
                table: "ExpResearcher",
                columns: new[] { "ExperimentId", "ResearcherId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpJudge",
                table: "ExpJudge",
                columns: new[] { "ExperimentId", "JudgeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ExpJudge_Judge_JudgeId",
                table: "ExpJudge",
                column: "JudgeId",
                principalTable: "Judge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpResearcher_Researcher_ResearcherId",
                table: "ExpResearcher",
                column: "ResearcherId",
                principalTable: "Researcher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
