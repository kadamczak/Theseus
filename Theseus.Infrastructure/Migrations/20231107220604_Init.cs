using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theseus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfessionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamSets_StaffMembers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mazes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Structure = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SolutionStartRow = table.Column<int>(type: "int", nullable: false),
                    SolutionStartColumn = table.Column<int>(type: "int", nullable: false),
                    Solution = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    StartDirection = table.Column<byte>(type: "tinyint", nullable: false),
                    EndDirection = table.Column<byte>(type: "tinyint", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mazes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mazes_StaffMembers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "ExamSetDto_MazeDto",
                columns: table => new
                {
                    ExamSetDtosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MazeDtosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSetDto_MazeDto", x => new { x.ExamSetDtosId, x.MazeDtosId });
                    table.ForeignKey(
                        name: "FK_ExamSetDto_MazeDto_ExamSets_ExamSetDtosId",
                        column: x => x.ExamSetDtosId,
                        principalTable: "ExamSets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExamSetDto_MazeDto_Mazes_MazeDtosId",
                        column: x => x.MazeDtosId,
                        principalTable: "Mazes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamSetDto_MazeDto_MazeDtosId",
                table: "ExamSetDto_MazeDto",
                column: "MazeDtosId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSets_OwnerId",
                table: "ExamSets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Mazes_OwnerId",
                table: "Mazes",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMemberDto_PatientDto_StaffMemberDtosId",
                table: "StaffMemberDto_PatientDto",
                column: "StaffMemberDtosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamSetDto_MazeDto");

            migrationBuilder.DropTable(
                name: "StaffMemberDto_PatientDto");

            migrationBuilder.DropTable(
                name: "ExamSets");

            migrationBuilder.DropTable(
                name: "Mazes");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "StaffMembers");
        }
    }
}
