using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedHub_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedTestTypesImplicitManyToManywithTestRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "test_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TestRequestTestType",
                columns: table => new
                {
                    TestRequestsId = table.Column<int>(type: "integer", nullable: false),
                    TestTypesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRequestTestType", x => new { x.TestRequestsId, x.TestTypesId });
                    table.ForeignKey(
                        name: "FK_TestRequestTestType_test_request_TestRequestsId",
                        column: x => x.TestRequestsId,
                        principalTable: "test_request",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestRequestTestType_test_type_TestTypesId",
                        column: x => x.TestTypesId,
                        principalTable: "test_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestRequestTestType_TestTypesId",
                table: "TestRequestTestType",
                column: "TestTypesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestRequestTestType");

            migrationBuilder.DropTable(
                name: "test_type");
        }
    }
}
