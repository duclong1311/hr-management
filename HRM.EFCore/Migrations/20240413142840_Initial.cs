using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BangCongs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TongTimeOT = table.Column<double>(type: "float", nullable: false),
                    TongSoNgayCong = table.Column<int>(type: "int", nullable: false),
                    NgayNghiPhep = table.Column<int>(type: "int", nullable: false),
                    NgayKhongNghiPhep = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangCongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BangLuongs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ThucLinh = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangLuongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoPhans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenBoPhan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoPhans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChucVus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HopDongs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaiHopDong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckHopDong = table.Column<bool>(type: "bit", nullable: false),
                    CheckChuKy = table.Column<bool>(type: "bit", nullable: false),
                    ChiTietHopDong = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HoSos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    SinhNgay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiSinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguyenQuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DanToc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KhoanKhauTrus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoanKhauTrus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KhoanThus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoanThus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhanSus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmailCongTy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    STK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaSoBHXH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaSoThue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayVaoLam = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LuongCoBan = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanSus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhucLoiVaPhuCaps",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhucLoiVaPhuCaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Permission = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ViTri",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenViTri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViTri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FingerprintCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BangCongs");

            migrationBuilder.DropTable(
                name: "BangLuongs");

            migrationBuilder.DropTable(
                name: "BoPhans");

            migrationBuilder.DropTable(
                name: "ChucVus");

            migrationBuilder.DropTable(
                name: "HopDongs");

            migrationBuilder.DropTable(
                name: "HoSos");

            migrationBuilder.DropTable(
                name: "KhoanKhauTrus");

            migrationBuilder.DropTable(
                name: "KhoanThus");

            migrationBuilder.DropTable(
                name: "NhanSus");

            migrationBuilder.DropTable(
                name: "PhucLoiVaPhuCaps");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ViTri");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
