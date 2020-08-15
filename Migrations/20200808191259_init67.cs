using Microsoft.EntityFrameworkCore.Migrations;

namespace DevDemo.Migrations
{
    public partial class init67 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "docUploads");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "docUploads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "docUploads");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "docUploads",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
