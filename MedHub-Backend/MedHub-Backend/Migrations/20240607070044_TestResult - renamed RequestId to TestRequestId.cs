using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedHub_Backend.Migrations
{
    /// <inheritdoc />
    public partial class TestResultrenamedRequestIdtoTestRequestId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_result_test_request_request_id",
                table: "test_result");

            migrationBuilder.RenameColumn(
                name: "request_id",
                table: "test_result",
                newName: "test_request_id");

            migrationBuilder.RenameIndex(
                name: "IX_test_result_request_id",
                table: "test_result",
                newName: "IX_test_result_test_request_id");

            migrationBuilder.AddForeignKey(
                name: "FK_test_result_test_request_test_request_id",
                table: "test_result",
                column: "test_request_id",
                principalTable: "test_request",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_result_test_request_test_request_id",
                table: "test_result");

            migrationBuilder.RenameColumn(
                name: "test_request_id",
                table: "test_result",
                newName: "request_id");

            migrationBuilder.RenameIndex(
                name: "IX_test_result_test_request_id",
                table: "test_result",
                newName: "IX_test_result_request_id");

            migrationBuilder.AddForeignKey(
                name: "FK_test_result_test_request_request_id",
                table: "test_result",
                column: "request_id",
                principalTable: "test_request",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
