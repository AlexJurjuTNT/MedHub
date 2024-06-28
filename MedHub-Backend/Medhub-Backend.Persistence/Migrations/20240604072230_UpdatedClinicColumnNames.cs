using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedHub_Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedClinicColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_medical_clinic_clinic_id",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_medical_clinic",
                table: "medical_clinic");

            migrationBuilder.RenameTable(
                name: "medical_clinic",
                newName: "clinic");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clinic",
                table: "clinic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_clinic_clinic_id",
                table: "user",
                column: "clinic_id",
                principalTable: "clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_clinic_clinic_id",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clinic",
                table: "clinic");

            migrationBuilder.RenameTable(
                name: "clinic",
                newName: "medical_clinic");

            migrationBuilder.AddPrimaryKey(
                name: "PK_medical_clinic",
                table: "medical_clinic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_medical_clinic_clinic_id",
                table: "user",
                column: "clinic_id",
                principalTable: "medical_clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
