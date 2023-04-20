using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParking.Migrations
{
    public partial class updateBaiXe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaiXe_NhanVien_NhanVien_Id",
                table: "BaiXe");

            migrationBuilder.DropIndex(
                name: "IX_BaiXe_NhanVien_Id",
                table: "BaiXe");

            migrationBuilder.DropColumn(
                name: "NhanVien_Id",
                table: "BaiXe");

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "BaiXe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BaiXe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NhanVienId",
                table: "BaiXe",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaiXe_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaiXe_NhanVienId",
                table: "BaiXe",
                column: "NhanVienId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaiXe_NhanVien_NhanVienId",
                table: "BaiXe",
                column: "NhanVienId",
                principalTable: "NhanVien",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaiXe_NhanVien_NhanVienId",
                table: "BaiXe");

            migrationBuilder.DropIndex(
                name: "IX_BaiXe_NhanVienId",
                table: "BaiXe");

            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "BaiXe");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BaiXe");

            migrationBuilder.DropColumn(
                name: "NhanVienId",
                table: "BaiXe");

            migrationBuilder.DropColumn(
                name: "BaiXe_Id",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "NhanVien_Id",
                table: "BaiXe",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BaiXe_NhanVien_Id",
                table: "BaiXe",
                column: "NhanVien_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaiXe_NhanVien_NhanVien_Id",
                table: "BaiXe",
                column: "NhanVien_Id",
                principalTable: "NhanVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
