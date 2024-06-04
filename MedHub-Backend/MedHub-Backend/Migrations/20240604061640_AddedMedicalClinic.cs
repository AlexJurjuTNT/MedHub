using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedHub_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedMedicalClinic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "clinic_id",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "medical_clinic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_clinic", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_clinic_id",
                table: "user",
                column: "clinic_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_medical_clinic_clinic_id",
                table: "user",
                column: "clinic_id",
                principalTable: "medical_clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_medical_clinic_clinic_id",
                table: "user");

            migrationBuilder.DropTable(
                name: "medical_clinic");

            migrationBuilder.DropIndex(
                name: "IX_user_clinic_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "clinic_id",
                table: "user");
        }
    }
}
