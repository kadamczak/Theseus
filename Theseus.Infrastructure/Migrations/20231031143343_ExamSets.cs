using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theseus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExamSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamSetDtoMazeDto",
                columns: table => new
                {
                    ExamSetsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MazeDtosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSetDtoMazeDto", x => new { x.ExamSetsId, x.MazeDtosId });
                    table.ForeignKey(
                        name: "FK_ExamSetDtoMazeDto_ExamSets_ExamSetsId",
                        column: x => x.ExamSetsId,
                        principalTable: "ExamSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamSetDtoMazeDto_Mazes_MazeDtosId",
                        column: x => x.MazeDtosId,
                        principalTable: "Mazes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamSetDtoMazeDto_MazeDtosId",
                table: "ExamSetDtoMazeDto",
                column: "MazeDtosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamSetDtoMazeDto");

            migrationBuilder.DropTable(
                name: "ExamSets");
        }
    }
}
