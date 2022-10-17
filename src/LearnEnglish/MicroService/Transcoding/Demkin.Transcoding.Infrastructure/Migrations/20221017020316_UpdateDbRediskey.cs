using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demkin.Transcoding.Infrastructure.Migrations
{
    public partial class UpdateDbRediskey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RedisKey",
                table: "TranscodeFile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RedisKey",
                table: "TranscodeFile");
        }
    }
}
