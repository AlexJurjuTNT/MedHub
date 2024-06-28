using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medhub_Backend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedFirstNameandFamilyNametoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "family_name",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "family_name",
                table: "user");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "user");
        }
    }
}
