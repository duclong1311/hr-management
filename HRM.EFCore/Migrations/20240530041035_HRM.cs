﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRM.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class HRM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BangLuongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Thang = table.Column<int>(type: "int", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LuongCoBan = table.Column<double>(type: "float", nullable: true),
                    TongSoNgayCong = table.Column<int>(type: "int", nullable: false),
                    TienCongThuong = table.Column<double>(type: "float", nullable: false),
                    LuongThucTe = table.Column<double>(type: "float", nullable: false),
                    NgayCongChuNhat = table.Column<int>(type: "int", nullable: false),
                    TienCongCN = table.Column<double>(type: "float", nullable: false),
                    SoGioOT = table.Column<double>(type: "float", nullable: false),
                    PhuCapAnTrua = table.Column<double>(type: "float", nullable: false),
                    PhuCapDiLai = table.Column<double>(type: "float", nullable: false),
                    PhuCap = table.Column<double>(type: "float", nullable: false),
                    TongLuong = table.Column<double>(type: "float", nullable: false),
                    BaoHiemXaHoi = table.Column<double>(type: "float", nullable: false),
                    BaoHiemYTe = table.Column<double>(type: "float", nullable: false),
                    SoGioDiMuonVeSom = table.Column<double>(type: "float", nullable: false),
                    TienCongNgayLe = table.Column<double>(type: "float", nullable: false),
                    TienTangCa = table.Column<double>(type: "float", nullable: false),
                    TienNgayNghiPhep = table.Column<double>(type: "float", nullable: false),
                    TienDiSomVeMuon = table.Column<double>(type: "float", nullable: false),
                    TienBaoHiem = table.Column<double>(type: "float", nullable: false),
                    UngLuong = table.Column<double>(type: "float", nullable: false),
                    TongKhauTru = table.Column<double>(type: "float", nullable: false),
                    ThucNhan = table.Column<double>(type: "float", nullable: false),
                    ThueTNCN = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangLuongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoPhans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChucVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhuCapChucVu = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucKhenThuongKyLuats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CapKhenThuongKyLuat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhThucKhenThuongKyLuat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucKhenThuongKyLuats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KyLuats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CapKyLuat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThamQuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoQuyetDinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianBanHanh = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KyLuats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuaTrinhCongTacs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TuNgayDenNgay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiCongTac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuaTrinhCongTacs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuaTrinhDaoTaos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TuNgayDenNgay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDaoTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NganhHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhThucDaoTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VanBangChungChi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuaTrinhDaoTaos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Permission = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhanSus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    NguyenQuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DanToc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TonGiao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CapNgay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KetNapDang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiketNapDang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoThich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STK = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSoBHXH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSoThue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LuongCoBan = table.Column<double>(type: "float", nullable: true),
                    BoPhanId = table.Column<int>(type: "int", nullable: true),
                    ChucVuId = table.Column<int>(type: "int", nullable: true),
                    QuaTrinhDaoTaoId = table.Column<int>(type: "int", nullable: true),
                    QuaTrinhCongTacId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanSus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhanSus_BoPhans_BoPhanId",
                        column: x => x.BoPhanId,
                        principalTable: "BoPhans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NhanSus_ChucVus_ChucVuId",
                        column: x => x.ChucVuId,
                        principalTable: "ChucVus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NhanSus_QuaTrinhCongTacs_QuaTrinhCongTacId",
                        column: x => x.QuaTrinhCongTacId,
                        principalTable: "QuaTrinhCongTacs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NhanSus_QuaTrinhDaoTaos_QuaTrinhDaoTaoId",
                        column: x => x.QuaTrinhDaoTaoId,
                        principalTable: "QuaTrinhDaoTaos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BangCongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thang = table.Column<int>(type: "int", nullable: true),
                    Nam = table.Column<int>(type: "int", nullable: true),
                    TongTimeOT = table.Column<double>(type: "float", nullable: true),
                    TongSoNgayCong = table.Column<int>(type: "int", nullable: true),
                    TongSoNgayCongCN = table.Column<int>(type: "int", nullable: true),
                    TongSoNgayCongNgayLe = table.Column<int>(type: "int", nullable: true),
                    DiMuonVeSom = table.Column<float>(type: "real", nullable: true),
                    NgayNghiPhep = table.Column<int>(type: "int", nullable: true),
                    NhanSuId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangCongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BangCongs_NhanSus_NhanSuId",
                        column: x => x.NhanSuId,
                        principalTable: "NhanSus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HopDongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhanSuId = table.Column<int>(type: "int", nullable: true),
                    SoHopDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoaiHopDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LuongCoBan = table.Column<double>(type: "float", nullable: true),
                    HeSoLuong = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HopDongs_NhanSus_NhanSuId",
                        column: x => x.NhanSuId,
                        principalTable: "NhanSus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KhenThuongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhanSuId = table.Column<int>(type: "int", nullable: true),
                    CapKhenThuongKyLuat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenHinhThuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayQuyetDinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoQuyetDinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhenThuongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhenThuongs_NhanSus_NhanSuId",
                        column: x => x.NhanSuId,
                        principalTable: "NhanSus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NhanSuChucVus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanSuId = table.Column<int>(type: "int", nullable: false),
                    ChucVuId = table.Column<int>(type: "int", nullable: false),
                    PhuCapChucVu = table.Column<double>(type: "float", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanSuChucVus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhanSuChucVus_ChucVus_ChucVuId",
                        column: x => x.ChucVuId,
                        principalTable: "ChucVus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhanSuChucVus_NhanSus_NhanSuId",
                        column: x => x.NhanSuId,
                        principalTable: "NhanSus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuanHeGiaDinhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoiQuanHe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoVaTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamSinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QueQuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgheNghiep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonViCongTac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NhanSuId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuanHeGiaDinhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuanHeGiaDinhs_NhanSus_NhanSuId",
                        column: x => x.NhanSuId,
                        principalTable: "NhanSus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UngLuongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoTienUng = table.Column<double>(type: "float", nullable: true),
                    NgayUngLuong = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhanSuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UngLuongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UngLuongs_NhanSus_NhanSuId",
                        column: x => x.NhanSuId,
                        principalTable: "NhanSus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BangCongNhanSus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Thang = table.Column<int>(type: "int", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    GioVao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioRa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BangCongId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangCongNhanSus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BangCongNhanSus_BangCongs_BangCongId",
                        column: x => x.BangCongId,
                        principalTable: "BangCongs",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "BoPhans",
                columns: new[] { "Id", "TenBoPhan" },
                values: new object[,]
                {
                    { 1, "Lập trình PC" },
                    { 2, "Cơ khí" },
                    { 3, "Kinh doanh" },
                    { 4, "Hành chính nhân sự" },
                    { 5, "Kế toán" },
                    { 6, "Kỹ thuật và hạ tầng" }
                });

            migrationBuilder.InsertData(
                table: "ChucVus",
                columns: new[] { "Id", "PhuCapChucVu", "TenChucVu" },
                values: new object[,]
                {
                    { 1, 500000.0, "Nhân viên" },
                    { 2, 1000000.0, "Quản lý" },
                    { 3, 2000000.0, "Trưởng phòng" },
                    { 4, 5000000.0, "Trưởng bộ phận" },
                    { 5, 10000000.0, "Phó giám đốc" },
                    { 6, 15000000.0, "Giám đốc" }
                });

            migrationBuilder.InsertData(
                table: "DanhMucKhenThuongKyLuats",
                columns: new[] { "Id", "CapKhenThuongKyLuat", "HinhThucKhenThuongKyLuat" },
                values: new object[,]
                {
                    { 1, "Cá nhân", "Thưởng tiền mặt" },
                    { 2, "Đội nhóm", "Thưởng tiền mặt" },
                    { 3, "Bộ phận", "Thưởng tiền mặt" },
                    { 4, "Cá nhân", "Trao giấy khen, bằng khen" },
                    { 5, "Đội nhóm", "Trao giấy khen, bằng khen" },
                    { 6, "Bộ phận", "Trao giấy khen, bằng khen" },
                    { 7, "Cá nhân", "Khiển trách bằng lời nói" },
                    { 8, "Đội nhóm", "Khiển trách bằng lời nói" },
                    { 9, "Bộ phận", "Khiển trách bằng lời nói" },
                    { 10, "Cá nhân", "Khiển trách bằng văn bản" },
                    { 11, "Đội nhóm", "Khiển trách bằng văn bản" },
                    { 12, "Bộ phận", "Khiển trách bằng văn bản" },
                    { 13, "Cá nhân", "Sa thải" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "Permission" },
                values: new object[,]
                {
                    { 1, "Admin", "" },
                    { 2, "Manager", "" },
                    { 3, "Compensation", "" },
                    { 4, "Staff", "" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[,]
                {
                    { 1, "admin", "admin", "??_?y??w+???l\0?_?n5?rY?????	?*", 1 },
                    { 2, "manager", "manager", "??_?y??w+???l\0?_?n5?rY?????	?*", 2 },
                    { 3, "compensation", "compensation", "??_?y??w+???l\0?_?n5?rY?????	?*", 3 },
                    { 4, "staff", "staff", "??_?y??w+???l\0?_?n5?rY?????	?*", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BangCongNhanSus_BangCongId",
                table: "BangCongNhanSus",
                column: "BangCongId");

            migrationBuilder.CreateIndex(
                name: "IX_BangCongs_NhanSuId",
                table: "BangCongs",
                column: "NhanSuId");

            migrationBuilder.CreateIndex(
                name: "IX_HopDongs_NhanSuId",
                table: "HopDongs",
                column: "NhanSuId");

            migrationBuilder.CreateIndex(
                name: "IX_KhenThuongs_NhanSuId",
                table: "KhenThuongs",
                column: "NhanSuId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanSuChucVus_ChucVuId",
                table: "NhanSuChucVus",
                column: "ChucVuId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanSuChucVus_NhanSuId",
                table: "NhanSuChucVus",
                column: "NhanSuId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanSus_BoPhanId",
                table: "NhanSus",
                column: "BoPhanId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanSus_ChucVuId",
                table: "NhanSus",
                column: "ChucVuId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanSus_QuaTrinhCongTacId",
                table: "NhanSus",
                column: "QuaTrinhCongTacId",
                unique: true,
                filter: "[QuaTrinhCongTacId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NhanSus_QuaTrinhDaoTaoId",
                table: "NhanSus",
                column: "QuaTrinhDaoTaoId",
                unique: true,
                filter: "[QuaTrinhDaoTaoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_QuanHeGiaDinhs_NhanSuId",
                table: "QuanHeGiaDinhs",
                column: "NhanSuId");

            migrationBuilder.CreateIndex(
                name: "IX_UngLuongs_NhanSuId",
                table: "UngLuongs",
                column: "NhanSuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BangCongNhanSus");

            migrationBuilder.DropTable(
                name: "BangLuongs");

            migrationBuilder.DropTable(
                name: "DanhMucKhenThuongKyLuats");

            migrationBuilder.DropTable(
                name: "HopDongs");

            migrationBuilder.DropTable(
                name: "KhenThuongs");

            migrationBuilder.DropTable(
                name: "KyLuats");

            migrationBuilder.DropTable(
                name: "NhanSuChucVus");

            migrationBuilder.DropTable(
                name: "QuanHeGiaDinhs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UngLuongs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BangCongs");

            migrationBuilder.DropTable(
                name: "NhanSus");

            migrationBuilder.DropTable(
                name: "BoPhans");

            migrationBuilder.DropTable(
                name: "ChucVus");

            migrationBuilder.DropTable(
                name: "QuaTrinhCongTacs");

            migrationBuilder.DropTable(
                name: "QuaTrinhDaoTaos");
        }
    }
}
