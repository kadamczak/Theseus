using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theseus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Dtos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSetDtoMazeDto_ExamSets_ExamSetsId",
                table: "ExamSetDtoMazeDto");

            migrationBuilder.DropTable(
                name: "PatientStaffMember");

            migrationBuilder.RenameColumn(
                name: "ExamSetsId",
                table: "ExamSetDtoMazeDto",
                newName: "ExamSetDtosId");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Mazes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ExamSets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PatientDtoStaffMemberDto",
                columns: table => new
                {
                    PatientDtosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffMemberDtosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDtoStaffMemberDto", x => new { x.PatientDtosId, x.StaffMemberDtosId });
                    table.ForeignKey(
                        name: "FK_PatientDtoStaffMemberDto_Patients_PatientDtosId",
                        column: x => x.PatientDtosId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientDtoStaffMemberDto_StaffMembers_StaffMemberDtosId",
                        column: x => x.StaffMemberDtosId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mazes_OwnerId",
                table: "Mazes",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSets_OwnerId",
                table: "ExamSets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDtoStaffMemberDto_StaffMemberDtosId",
                table: "PatientDtoStaffMemberDto",
                column: "StaffMemberDtosId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSetDtoMazeDto_ExamSets_ExamSetDtosId",
                table: "ExamSetDtoMazeDto",
                column: "ExamSetDtosId",
                principalTable: "ExamSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSets_StaffMembers_OwnerId",
                table: "ExamSets",
                column: "OwnerId",
                principalTable: "StaffMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mazes_StaffMembers_OwnerId",
                table: "Mazes",
                column: "OwnerId",
                principalTable: "StaffMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSetDtoMazeDto_ExamSets_ExamSetDtosId",
                table: "ExamSetDtoMazeDto");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamSets_StaffMembers_OwnerId",
                table: "ExamSets");

            migrationBuilder.DropForeignKey(
                name: "FK_Mazes_StaffMembers_OwnerId",
                table: "Mazes");

            migrationBuilder.DropTable(
                name: "PatientDtoStaffMemberDto");

            migrationBuilder.DropIndex(
                name: "IX_Mazes_OwnerId",
                table: "Mazes");

            migrationBuilder.DropIndex(
                name: "IX_ExamSets_OwnerId",
                table: "ExamSets");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Mazes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ExamSets");

            migrationBuilder.RenameColumn(
                name: "ExamSetDtosId",
                table: "ExamSetDtoMazeDto",
                newName: "ExamSetsId");

            migrationBuilder.CreateTable(
                name: "PatientStaffMember",
                columns: table => new
                {
                    PatientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffMembersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientStaffMember", x => new { x.PatientsId, x.StaffMembersId });
                    table.ForeignKey(
                        name: "FK_PatientStaffMember_Patients_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientStaffMember_StaffMembers_StaffMembersId",
                        column: x => x.StaffMembersId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientStaffMember_StaffMembersId",
                table: "PatientStaffMember",
                column: "StaffMembersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSetDtoMazeDto_ExamSets_ExamSetsId",
                table: "ExamSetDtoMazeDto",
                column: "ExamSetsId",
                principalTable: "ExamSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
