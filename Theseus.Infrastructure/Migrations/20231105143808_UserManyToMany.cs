using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theseus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientStaffMember");
        }
    }
}
