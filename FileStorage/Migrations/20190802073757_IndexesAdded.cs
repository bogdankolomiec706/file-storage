using Microsoft.EntityFrameworkCore.Migrations;

namespace FileStorage.Migrations
{
    public partial class IndexesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Index",
                table: "Files",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_Index",
                table: "Files",
                column: "Index",
                unique: true,
                filter: "[Index] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Files_Index",
                table: "Files");

            migrationBuilder.AlterColumn<string>(
                name: "Index",
                table: "Files",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
