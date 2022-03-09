using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POCloudAPI.Migrations
{
    public partial class removeAPIid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "APIUserId",
                table: "Files");

            migrationBuilder.AlterColumn<byte[]>(
                name: "FileStreamData",
                table: "Files",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "FileStreamData",
                table: "Files",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "APIUserId",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
