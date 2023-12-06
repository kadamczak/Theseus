using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theseus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExamStageName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSteps_ExamStages_StageDtosId",
                table: "ExamSteps");

            migrationBuilder.RenameColumn(
                name: "StageDtosId",
                table: "ExamSteps",
                newName: "StageDtoId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamSteps_StageDtosId",
                table: "ExamSteps",
                newName: "IX_ExamSteps_StageDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSteps_ExamStages_StageDtoId",
                table: "ExamSteps",
                column: "StageDtoId",
                principalTable: "ExamStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSteps_ExamStages_StageDtoId",
                table: "ExamSteps");

            migrationBuilder.RenameColumn(
                name: "StageDtoId",
                table: "ExamSteps",
                newName: "StageDtosId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamSteps_StageDtoId",
                table: "ExamSteps",
                newName: "IX_ExamSteps_StageDtosId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSteps_ExamStages_StageDtosId",
                table: "ExamSteps",
                column: "StageDtosId",
                principalTable: "ExamStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
