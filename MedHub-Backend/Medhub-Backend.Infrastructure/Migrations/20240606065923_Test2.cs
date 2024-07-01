using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedHub_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_request_patient_patient_id",
                table: "test_request");

            migrationBuilder.DropIndex(
                name: "IX_patient_user_id",
                table: "patient");

            migrationBuilder.RenameColumn(
                name: "patient_id",
                table: "test_request",
                newName: "patient_id1");

            migrationBuilder.RenameIndex(
                name: "IX_test_request_patient_id",
                table: "test_request",
                newName: "IX_test_request_patient_id1");

            migrationBuilder.CreateIndex(
                name: "IX_patient_user_id",
                table: "patient",
                column: "user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_test_request_user_patient_id1",
                table: "test_request",
                column: "patient_id1",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_request_user_patient_id1",
                table: "test_request");

            migrationBuilder.DropIndex(
                name: "IX_patient_user_id",
                table: "patient");

            migrationBuilder.RenameColumn(
                name: "patient_id1",
                table: "test_request",
                newName: "patient_id");

            migrationBuilder.RenameIndex(
                name: "IX_test_request_patient_id1",
                table: "test_request",
                newName: "IX_test_request_patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_patient_user_id",
                table: "patient",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_test_request_patient_patient_id",
                table: "test_request",
                column: "patient_id",
                principalTable: "patient",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
