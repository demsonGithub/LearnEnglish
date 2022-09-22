using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demkin.Listen.Infrastructure.Migrations
{
    public partial class UpdateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MultipleLanguageTitle_ChineseTitle",
                table: "Category",
                newName: "ChineseTitle");

            migrationBuilder.RenameColumn(
                name: "MultipleLanguageTitle_English",
                table: "Category",
                newName: "EnglishTitle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChineseTitle",
                table: "Category",
                newName: "MultipleLanguageTitle_ChineseTitle");

            migrationBuilder.RenameColumn(
                name: "EnglishTitle",
                table: "Category",
                newName: "MultipleLanguageTitle_English");
        }
    }
}
