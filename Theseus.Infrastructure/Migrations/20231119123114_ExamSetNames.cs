using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theseus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExamSetNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ExamSets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ExamSets");
        }
    }
}
