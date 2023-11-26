using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theseus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDefaultGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSetDto_MazeDto_ExamSets_ExamSetDtosId",
                table: "ExamSetDto_MazeDto");

            migrationBuilder.DropColumn(
                name: "Default",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSetDto_MazeDto_ExamSets_ExamSetDtosId",
                table: "ExamSetDto_MazeDto",
                column: "ExamSetDtosId",
                principalTable: "ExamSets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSetDto_MazeDto_ExamSets_ExamSetDtosId",
                table: "ExamSetDto_MazeDto");

            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "Groups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSetDto_MazeDto_ExamSets_ExamSetDtosId",
                table: "ExamSetDto_MazeDto",
                column: "ExamSetDtosId",
                principalTable: "ExamSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
