using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demkin.Listen.Infrastructure.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MultipleLanguageTitle_ChineseTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MultipleLanguageTitle_English = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CoverUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
