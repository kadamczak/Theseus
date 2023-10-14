using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theseus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MazeWithSolution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Mazes",
                newName: "Structure");

            migrationBuilder.AddColumn<byte>(
                name: "EndDirection",
                table: "Mazes",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte[]>(
                name: "Solution",
                table: "Mazes",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "SolutionStartColumn",
                table: "Mazes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SolutionStartRow",
                table: "Mazes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "StartDirection",
                table: "Mazes",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDirection",
                table: "Mazes");

            migrationBuilder.DropColumn(
                name: "Solution",
                table: "Mazes");

            migrationBuilder.DropColumn(
                name: "SolutionStartColumn",
                table: "Mazes");

            migrationBuilder.DropColumn(
                name: "SolutionStartRow",
                table: "Mazes");

            migrationBuilder.DropColumn(
                name: "StartDirection",
                table: "Mazes");

            migrationBuilder.RenameColumn(
                name: "Structure",
                table: "Mazes",
                newName: "Data");
        }
    }
}
