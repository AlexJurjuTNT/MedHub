using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedHub_Backend.Migrations
{
    /// <inheritdoc />
    public partial class GenderEnumisseenasastring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_test_result_request_id",
                table: "test_result");

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "patient",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_test_result_request_id",
                table: "test_result",
                column: "request_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_test_result_request_id",
                table: "test_result");

            migrationBuilder.AlterColumn<int>(
                name: "gender",
                table: "patient",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_test_result_request_id",
                table: "test_result",
                column: "request_id");
        }
    }
}
