using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medhub_Backend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class LaboratoryandTestTypeConfiguredManytoMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_type_laboratory_LaboratoryId",
                table: "test_type");

            migrationBuilder.DropIndex(
                name: "IX_test_type_LaboratoryId",
                table: "test_type");

            migrationBuilder.DropColumn(
                name: "LaboratoryId",
                table: "test_type");

            migrationBuilder.CreateTable(
                name: "LaboratoryTestType",
                columns: table => new
                {
                    LaboratoriesId = table.Column<int>(type: "integer", nullable: false),
                    TestTypesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryTestType", x => new { x.LaboratoriesId, x.TestTypesId });
                    table.ForeignKey(
                        name: "FK_LaboratoryTestType_laboratory_LaboratoriesId",
                        column: x => x.LaboratoriesId,
                        principalTable: "laboratory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaboratoryTestType_test_type_TestTypesId",
                        column: x => x.TestTypesId,
                        principalTable: "test_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryTestType_TestTypesId",
                table: "LaboratoryTestType",
                column: "TestTypesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LaboratoryTestType");

            migrationBuilder.AddColumn<int>(
                name: "LaboratoryId",
                table: "test_type",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_test_type_LaboratoryId",
                table: "test_type",
                column: "LaboratoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_test_type_laboratory_LaboratoryId",
                table: "test_type",
                column: "LaboratoryId",
                principalTable: "laboratory",
                principalColumn: "Id");
        }
    }
}
