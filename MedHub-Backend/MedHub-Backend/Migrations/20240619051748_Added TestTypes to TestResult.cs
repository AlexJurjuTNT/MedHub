using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedHub_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedTestTypestoTestResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestResultId",
                table: "test_type",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "completion_date",
                table: "test_result",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "request_date",
                table: "test_request",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_type_test_result_TestResultId",
                table: "test_type");

            migrationBuilder.DropIndex(
                name: "IX_test_type_TestResultId",
                table: "test_type");

            migrationBuilder.DropColumn(
                name: "TestResultId",
                table: "test_type");

            migrationBuilder.AlterColumn<DateTime>(
                name: "completion_date",
                table: "test_result",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "request_date",
                table: "test_request",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
