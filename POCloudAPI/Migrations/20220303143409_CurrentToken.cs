using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POCloudAPI.Migrations
{
    public partial class CurrentToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "created",
                table: "Users",
                newName: "Created");

            migrationBuilder.AddColumn<string>(
                name: "CurrentToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentToken",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Users",
                newName: "created");
        }
    }
}
