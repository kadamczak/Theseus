using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theseus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Groups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffMemberDto_PatientDto");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupDtoId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamSet_Group",
                columns: table => new
                {
                    ExamSetDtosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupDtosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSet_Group", x => new { x.ExamSetDtosId, x.GroupDtosId });
                    table.ForeignKey(
                        name: "FK_ExamSet_Group_ExamSets_ExamSetDtosId",
                        column: x => x.ExamSetDtosId,
                        principalTable: "ExamSets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExamSet_Group_Groups_GroupDtosId",
                        column: x => x.GroupDtosId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaffMember_Group",
                columns: table => new
                {
                    GroupDtosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffMemberDtosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMember_Group", x => new { x.GroupDtosId, x.StaffMemberDtosId });
                    table.ForeignKey(
                        name: "FK_StaffMember_Group_Groups_GroupDtosId",
                        column: x => x.GroupDtosId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffMember_Group_StaffMembers_StaffMemberDtosId",
                        column: x => x.StaffMemberDtosId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_GroupDtoId",
                table: "Patients",
                column: "GroupDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSet_Group_GroupDtosId",
                table: "ExamSet_Group",
                column: "GroupDtosId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_Group_StaffMemberDtosId",
                table: "StaffMember_Group",
                column: "StaffMemberDtosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Groups_GroupDtoId",
                table: "Patients",
                column: "GroupDtoId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Groups_GroupDtoId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "ExamSet_Group");

            migrationBuilder.DropTable(
                name: "StaffMember_Group");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Patients_GroupDtoId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "GroupDtoId",
                table: "Patients");

            migrationBuilder.CreateTable(
                name: "StaffMemberDto_PatientDto",
                columns: table => new
                {
                    PatientDtosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffMemberDtosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMemberDto_PatientDto", x => new { x.PatientDtosId, x.StaffMemberDtosId });
                    table.ForeignKey(
                        name: "FK_StaffMemberDto_PatientDto_Patients_PatientDtosId",
                        column: x => x.PatientDtosId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffMemberDto_PatientDto_StaffMembers_StaffMemberDtosId",
                        column: x => x.StaffMemberDtosId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffMemberDto_PatientDto_StaffMemberDtosId",
                table: "StaffMemberDto_PatientDto",
                column: "StaffMemberDtosId");
        }
    }
}
