using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medhub_Backend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedClinictoTestRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "clinic_id",
                table: "test_request",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_test_request_clinic_id",
                table: "test_request",
                column: "clinic_id");

            migrationBuilder.AddForeignKey(
                name: "FK_test_request_clinic_clinic_id",
                table: "test_request",
                column: "clinic_id",
                principalTable: "clinic",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_request_clinic_clinic_id",
                table: "test_request");

            migrationBuilder.DropIndex(
                name: "IX_test_request_clinic_id",
                table: "test_request");

            migrationBuilder.DropColumn(
                name: "clinic_id",
                table: "test_request");
        }
    }
}
