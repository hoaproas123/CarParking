using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParking.Migrations
{
    public partial class addAppuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleNames",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleNames",
                table: "AspNetUsers");
        }
    }
}
