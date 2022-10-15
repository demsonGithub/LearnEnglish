using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demkin.Transcoding.Infrastructure.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TranscodeFile",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    SourceUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSHA256Hash = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: true),
                    TargetFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TranscodingUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranscodeStatus = table.Column<int>(type: "int", nullable: false),
                    LogMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranscodeFile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TranscodeFile_FileSizeBytes_FileSHA256Hash",
                table: "TranscodeFile",
                columns: new[] { "FileSizeBytes", "FileSHA256Hash" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TranscodeFile");
        }
    }
}
