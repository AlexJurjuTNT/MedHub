using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedHub_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Removedinheritancefrompatientusecompositionwithuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patient_user_id",
                table: "patient");

            migrationBuilder.DropColumn(
                name: "city",
                table: "patient");

            migrationBuilder.DropColumn(
                name: "street",
                table: "patient");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "patient",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "patient",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_patient_user_id",
                table: "patient",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_patient_user_user_id",
                table: "patient",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patient_user_user_id",
                table: "patient");

            migrationBuilder.DropIndex(
                name: "IX_patient_user_id",
                table: "patient");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "patient");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "patient",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "patient",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "street",
                table: "patient",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_patient_user_id",
                table: "patient",
                column: "id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
