using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedHub_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedLaboratories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LaboratoryId",
                table: "test_type",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "laboratory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    location = table.Column<string>(type: "text", nullable: false),
                    clinic_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laboratory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_laboratory_clinic_clinic_id",
                        column: x => x.clinic_id,
                        principalTable: "clinic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_test_type_LaboratoryId",
                table: "test_type",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_laboratory_clinic_id",
                table: "laboratory",
                column: "clinic_id");

            migrationBuilder.AddForeignKey(
                name: "FK_test_type_laboratory_LaboratoryId",
                table: "test_type",
                column: "LaboratoryId",
                principalTable: "laboratory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_type_laboratory_LaboratoryId",
                table: "test_type");

            migrationBuilder.DropTable(
                name: "laboratory");

            migrationBuilder.DropIndex(
                name: "IX_test_type_LaboratoryId",
                table: "test_type");

            migrationBuilder.DropColumn(
                name: "LaboratoryId",
                table: "test_type");
        }
    }
}
