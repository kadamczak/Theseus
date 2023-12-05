using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theseus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_ExamSets_ExamSetId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Patients_PatientId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamStages_Exams_ExamId",
                table: "ExamStages");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamSteps_ExamStages_StageId",
                table: "ExamSteps");

            migrationBuilder.RenameColumn(
                name: "StageId",
                table: "ExamSteps",
                newName: "StageDtosId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamSteps_StageId",
                table: "ExamSteps",
                newName: "IX_ExamSteps_StageDtosId");

            migrationBuilder.RenameColumn(
                name: "ExamId",
                table: "ExamStages",
                newName: "ExamDtoId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamStages_ExamId",
                table: "ExamStages",
                newName: "IX_ExamStages_ExamDtoId");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Exams",
                newName: "PatientDtoId");

            migrationBuilder.RenameColumn(
                name: "ExamSetId",
                table: "Exams",
                newName: "ExamSetDtoId");

            migrationBuilder.RenameIndex(
                name: "IX_Exams_PatientId",
                table: "Exams",
                newName: "IX_Exams_PatientDtoId");

            migrationBuilder.RenameIndex(
                name: "IX_Exams_ExamSetId",
                table: "Exams",
                newName: "IX_Exams_ExamSetDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_ExamSets_ExamSetDtoId",
                table: "Exams",
                column: "ExamSetDtoId",
                principalTable: "ExamSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Patients_PatientDtoId",
                table: "Exams",
                column: "PatientDtoId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamStages_Exams_ExamDtoId",
                table: "ExamStages",
                column: "ExamDtoId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSteps_ExamStages_StageDtosId",
                table: "ExamSteps",
                column: "StageDtosId",
                principalTable: "ExamStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_ExamSets_ExamSetDtoId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Patients_PatientDtoId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamStages_Exams_ExamDtoId",
                table: "ExamStages");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamSteps_ExamStages_StageDtosId",
                table: "ExamSteps");

            migrationBuilder.RenameColumn(
                name: "StageDtosId",
                table: "ExamSteps",
                newName: "StageId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamSteps_StageDtosId",
                table: "ExamSteps",
                newName: "IX_ExamSteps_StageId");

            migrationBuilder.RenameColumn(
                name: "ExamDtoId",
                table: "ExamStages",
                newName: "ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamStages_ExamDtoId",
                table: "ExamStages",
                newName: "IX_ExamStages_ExamId");

            migrationBuilder.RenameColumn(
                name: "PatientDtoId",
                table: "Exams",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "ExamSetDtoId",
                table: "Exams",
                newName: "ExamSetId");

            migrationBuilder.RenameIndex(
                name: "IX_Exams_PatientDtoId",
                table: "Exams",
                newName: "IX_Exams_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Exams_ExamSetDtoId",
                table: "Exams",
                newName: "IX_Exams_ExamSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_ExamSets_ExamSetId",
                table: "Exams",
                column: "ExamSetId",
                principalTable: "ExamSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Patients_PatientId",
                table: "Exams",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamStages_Exams_ExamId",
                table: "ExamStages",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSteps_ExamStages_StageId",
                table: "ExamSteps",
                column: "StageId",
                principalTable: "ExamStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
