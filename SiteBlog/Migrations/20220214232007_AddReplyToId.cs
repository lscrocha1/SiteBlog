using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteBlog.Migrations
{
    public partial class AddReplyToId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReplyingToId",
                table: "Replies",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplyingToId",
                table: "Replies");
        }
    }
}
