using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileStorage.Migrations
{
    public partial class Categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Files_Index",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Cateory",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Files");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Files",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CateoryId",
                table: "Files",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FullIndex",
                table: "Files",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FileCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_CategoryId",
                table: "Files",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FullIndex",
                table: "Files",
                column: "FullIndex",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileCategories_Name",
                table: "FileCategories",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FileCategories_CategoryId",
                table: "Files",
                column: "CategoryId",
                principalTable: "FileCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileCategories_CategoryId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "FileCategories");

            migrationBuilder.DropIndex(
                name: "IX_Files_CategoryId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_FullIndex",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CateoryId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FullIndex",
                table: "Files");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Files",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "Cateory",
                table: "Files",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Index",
                table: "Files",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Files_Index",
                table: "Files",
                column: "Index",
                unique: true);
        }
    }
}
