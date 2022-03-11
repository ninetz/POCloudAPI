using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POCloudAPI.Migrations
{
    public partial class filechangeheaders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MetaData",
                table: "Files",
                newName: "ContentType");

            migrationBuilder.AddColumn<string>(
                name: "ContentDisposition",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentDisposition",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "Files",
                newName: "MetaData");
        }
    }
}
