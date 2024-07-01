using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medhub_Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixedManyToManybetweenTestTypeandTestResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_type_test_result_TestResultId",
                table: "test_type");

            migrationBuilder.DropForeignKey(
                name: "FK_TestRequestTestType_test_request_TestRequestsId",
                table: "TestRequestTestType");

            migrationBuilder.DropForeignKey(
                name: "FK_TestRequestTestType_test_type_TestTypesId",
                table: "TestRequestTestType");

            migrationBuilder.DropIndex(
                name: "IX_test_type_TestResultId",
                table: "test_type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestRequestTestType",
                table: "TestRequestTestType");

            migrationBuilder.DropColumn(
                name: "TestResultId",
                table: "test_type");

            migrationBuilder.RenameTable(
                name: "TestRequestTestType",
                newName: "test_request_test_type");

            migrationBuilder.RenameIndex(
                name: "IX_TestRequestTestType_TestTypesId",
                table: "test_request_test_type",
                newName: "IX_test_request_test_type_TestTypesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_test_request_test_type",
                table: "test_request_test_type",
                columns: new[] { "TestRequestsId", "TestTypesId" });

            migrationBuilder.CreateTable(
                name: "test_result_test_type",
                columns: table => new
                {
                    TestResultsId = table.Column<int>(type: "integer", nullable: false),
                    TestTypesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_result_test_type", x => new { x.TestResultsId, x.TestTypesId });
                    table.ForeignKey(
                        name: "FK_test_result_test_type_test_result_TestResultsId",
                        column: x => x.TestResultsId,
                        principalTable: "test_result",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_test_result_test_type_test_type_TestTypesId",
                        column: x => x.TestTypesId,
                        principalTable: "test_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_test_result_test_type_TestTypesId",
                table: "test_result_test_type",
                column: "TestTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_test_request_test_type_test_request_TestRequestsId",
                table: "test_request_test_type",
                column: "TestRequestsId",
                principalTable: "test_request",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_test_request_test_type_test_type_TestTypesId",
                table: "test_request_test_type",
                column: "TestTypesId",
                principalTable: "test_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_request_test_type_test_request_TestRequestsId",
                table: "test_request_test_type");

            migrationBuilder.DropForeignKey(
                name: "FK_test_request_test_type_test_type_TestTypesId",
                table: "test_request_test_type");

            migrationBuilder.DropTable(
                name: "test_result_test_type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_test_request_test_type",
                table: "test_request_test_type");

            migrationBuilder.RenameTable(
                name: "test_request_test_type",
                newName: "TestRequestTestType");

            migrationBuilder.RenameIndex(
                name: "IX_test_request_test_type_TestTypesId",
                table: "TestRequestTestType",
                newName: "IX_TestRequestTestType_TestTypesId");

            migrationBuilder.AddColumn<int>(
                name: "TestResultId",
                table: "test_type",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestRequestTestType",
                table: "TestRequestTestType",
                columns: new[] { "TestRequestsId", "TestTypesId" });

            migrationBuilder.CreateIndex(
                name: "IX_test_type_TestResultId",
                table: "test_type",
                column: "TestResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_test_type_test_result_TestResultId",
                table: "test_type",
                column: "TestResultId",
                principalTable: "test_result",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestRequestTestType_test_request_TestRequestsId",
                table: "TestRequestTestType",
                column: "TestRequestsId",
                principalTable: "test_request",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestRequestTestType_test_type_TestTypesId",
                table: "TestRequestTestType",
                column: "TestTypesId",
                principalTable: "test_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
