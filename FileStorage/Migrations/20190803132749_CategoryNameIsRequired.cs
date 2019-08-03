using Microsoft.EntityFrameworkCore.Migrations;

namespace FileStorage.Migrations
{
    public partial class CategoryNameIsRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileCategories_Name",
                table: "FileCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FileCategories",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileCategories_Name",
                table: "FileCategories",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileCategories_Name",
                table: "FileCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FileCategories",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.CreateIndex(
                name: "IX_FileCategories_Name",
                table: "FileCategories",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
