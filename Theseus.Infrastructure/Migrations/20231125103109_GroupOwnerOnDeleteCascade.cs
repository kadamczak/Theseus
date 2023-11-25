using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theseus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GroupOwnerOnDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_StaffMembers_OwnerId",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_StaffMembers_OwnerId",
                table: "Groups",
                column: "OwnerId",
                principalTable: "StaffMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_StaffMembers_OwnerId",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_StaffMembers_OwnerId",
                table: "Groups",
                column: "OwnerId",
                principalTable: "StaffMembers",
                principalColumn: "Id");
        }
    }
}
