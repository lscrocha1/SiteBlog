using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteBlog.Migrations
{
    public partial class AddEnPtFieldsPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Posts",
                newName: "PtTitle");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Posts",
                newName: "PtDescription");

            migrationBuilder.AddColumn<string>(
                name: "EnDescription",
                table: "Posts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnTitle",
                table: "Posts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnDescription",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "EnTitle",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "PtTitle",
                table: "Posts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "PtDescription",
                table: "Posts",
                newName: "Description");
        }
    }
}
