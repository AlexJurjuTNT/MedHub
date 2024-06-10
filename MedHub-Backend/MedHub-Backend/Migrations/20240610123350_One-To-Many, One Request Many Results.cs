using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedHub_Backend.Migrations
{
    /// <inheritdoc />
    public partial class OneToManyOneRequestManyResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_test_result_test_request_id",
                table: "test_result");

            migrationBuilder.CreateIndex(
                name: "IX_test_result_test_request_id",
                table: "test_result",
                column: "test_request_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_test_result_test_request_id",
                table: "test_result");

            migrationBuilder.CreateIndex(
                name: "IX_test_result_test_request_id",
                table: "test_result",
                column: "test_request_id",
                unique: true);
        }
    }
}
