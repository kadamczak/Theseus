using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theseus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Exams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_StaffMembers_OwnerId",
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
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupDtoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Groups_GroupDtoId",
                        column: x => x.GroupDtoId,
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

            migrationBuilder.CreateTable(
                name: "ExamSetDtos_MazeDtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamSetDtoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MazeDtoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSetDtos_MazeDtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamSetDtos_MazeDtos_ExamSets_ExamSetDtoId",
                        column: x => x.ExamSetDtoId,
                        principalTable: "ExamSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamSetDtos_MazeDtos_Mazes_MazeDtoId",
                        column: x => x.MazeDtoId,
                        principalTable: "Mazes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_ExamSets_ExamSetId",
                        column: x => x.ExamSetId,
                        principalTable: "ExamSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exams_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamStages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamStages_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    StepTaken = table.Column<int>(type: "int", nullable: false),
                    TimeBeforeStep = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamSteps_ExamStages_StageId",
                        column: x => x.StageId,
                        principalTable: "ExamStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ExamSetId",
                table: "Exams",
                column: "ExamSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_PatientId",
                table: "Exams",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSet_Group_GroupDtosId",
                table: "ExamSet_Group",
                column: "GroupDtosId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSetDtos_MazeDtos_ExamSetDtoId",
                table: "ExamSetDtos_MazeDtos",
                column: "ExamSetDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSetDtos_MazeDtos_MazeDtoId",
                table: "ExamSetDtos_MazeDtos",
                column: "MazeDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSets_OwnerId",
                table: "ExamSets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamStages_ExamId",
                table: "ExamStages",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSteps_StageId",
                table: "ExamSteps",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_OwnerId",
                table: "Groups",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Mazes_OwnerId",
                table: "Mazes",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_GroupDtoId",
                table: "Patients",
                column: "GroupDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_Group_StaffMemberDtosId",
                table: "StaffMember_Group",
                column: "StaffMemberDtosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamSet_Group");

            migrationBuilder.DropTable(
                name: "ExamSetDtos_MazeDtos");

            migrationBuilder.DropTable(
                name: "ExamSteps");

            migrationBuilder.DropTable(
                name: "StaffMember_Group");

            migrationBuilder.DropTable(
                name: "Mazes");

            migrationBuilder.DropTable(
                name: "ExamStages");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "ExamSets");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "StaffMembers");
        }
    }
}
