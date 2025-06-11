using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ucode.Api.Migrations
{
    /// <inheritdoc />
    public partial class v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Enrollment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "Enrollment",
                type: "SMALLINT",
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Enrollment");
        }
    }
}
