using Microsoft.EntityFrameworkCore.Migrations;

namespace CJEngine.Migrations
{
    public partial class MyMigratrion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairing_Judge_JudgeId",
                table: "Pairing");

            migrationBuilder.AlterColumn<int>(
                name: "JudgeId",
                table: "Pairing",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "JudgeLoginID",
                table: "Pairing",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairing_Judge_JudgeId",
                table: "Pairing",
                column: "JudgeId",
                principalTable: "Judge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairing_Judge_JudgeId",
                table: "Pairing");

            migrationBuilder.DropColumn(
                name: "JudgeLoginID",
                table: "Pairing");

            migrationBuilder.AlterColumn<int>(
                name: "JudgeId",
                table: "Pairing",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairing_Judge_JudgeId",
                table: "Pairing",
                column: "JudgeId",
                principalTable: "Judge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
