using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demkin.FileOperation.Infrastructure.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadFileInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    FileSHA256Hash = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    RemoteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackupUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadFileInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadFileInfo_FileSizeBytes_FileSHA256Hash",
                table: "UploadFileInfo",
                columns: new[] { "FileSizeBytes", "FileSHA256Hash" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadFileInfo");
        }
    }
}
