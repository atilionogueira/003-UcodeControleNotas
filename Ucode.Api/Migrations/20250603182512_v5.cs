using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ucode.Api.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityRole_IdentityUser_UserId",
                table: "IdentityRole");

            migrationBuilder.DropIndex(
                name: "IX_IdentityRole_UserId",
                table: "IdentityRole");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "IdentityRole");

            migrationBuilder.AddColumn<string>(
                name: "EnrollmentNumber",
                table: "Enrollment",
                type: "VARCHAR(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnrollmentNumber",
                table: "Enrollment");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "IdentityRole",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRole_UserId",
                table: "IdentityRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRole_IdentityUser_UserId",
                table: "IdentityRole",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }
    }
}
