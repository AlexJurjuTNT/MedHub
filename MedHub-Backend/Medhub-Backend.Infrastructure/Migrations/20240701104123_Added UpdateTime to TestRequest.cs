using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medhub_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUpdateTimetoTestRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "update_time",
                table: "test_request",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "update_time",
                table: "test_request");
        }
    }
}
