using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedHub_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedLaboratorytoTestRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "laboratory_id",
                table: "test_request",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_test_request_laboratory_id",
                table: "test_request",
                column: "laboratory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_test_request_laboratory_laboratory_id",
                table: "test_request",
                column: "laboratory_id",
                principalTable: "laboratory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_request_laboratory_laboratory_id",
                table: "test_request");

            migrationBuilder.DropIndex(
                name: "IX_test_request_laboratory_id",
                table: "test_request");

            migrationBuilder.DropColumn(
                name: "laboratory_id",
                table: "test_request");
        }
    }
}
