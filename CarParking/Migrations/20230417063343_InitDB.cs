using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParking.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dChi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhanVien_Account_UserName",
                        column: x => x.UserName,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BaiXe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllSlot = table.Column<int>(type: "int", nullable: false),
                    RemainingSlot = table.Column<int>(type: "int", nullable: false),
                    NhanVien_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiXe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaiXe_NhanVien_NhanVien_Id",
                        column: x => x.NhanVien_Id,
                        principalTable: "NhanVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    timeIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    timeOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BaiXe_Id = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhachHang_BaiXe_BaiXe_Id",
                        column: x => x.BaiXe_Id,
                        principalTable: "BaiXe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaiXe_NhanVien_Id",
                table: "BaiXe",
                column: "NhanVien_Id");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_BaiXe_Id",
                table: "KhachHang",
                column: "BaiXe_Id");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_UserName",
                table: "NhanVien",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "BaiXe");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
