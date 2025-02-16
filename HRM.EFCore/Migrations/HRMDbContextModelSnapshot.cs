﻿// <auto-generated />
using System;
using HRM.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HRM.EFCore.Migrations
{
    [DbContext(typeof(HRMDbContext))]
    partial class HRMDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HRM.Domain.Models.BangCong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("DiMuonVeSom")
                        .HasColumnType("real");

                    b.Property<string>("HoTen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaNhanVien")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Nam")
                        .HasColumnType("int");

                    b.Property<int?>("NgayNghiPhep")
                        .HasColumnType("int");

                    b.Property<int?>("NhanSuId")
                        .HasColumnType("int");

                    b.Property<int?>("Thang")
                        .HasColumnType("int");

                    b.Property<int?>("TongSoNgayCong")
                        .HasColumnType("int");

                    b.Property<int?>("TongSoNgayCongCN")
                        .HasColumnType("int");

                    b.Property<int?>("TongSoNgayCongNgayLe")
                        .HasColumnType("int");

                    b.Property<double?>("TongTimeOT")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("NhanSuId");

                    b.ToTable("BangCongs");
                });

            modelBuilder.Entity("HRM.Domain.Models.BangCongNhanSu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BangCongId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("GioRa")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("GioVao")
                        .HasColumnType("datetime2");

                    b.Property<string>("HoTen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaNhanVien")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Nam")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Ngay")
                        .HasColumnType("datetime2");

                    b.Property<int>("Thang")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BangCongId");

                    b.ToTable("BangCongNhanSus");
                });

            modelBuilder.Entity("HRM.Domain.Models.BangLuong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("BaoHiemXaHoi")
                        .HasColumnType("float");

                    b.Property<double>("BaoHiemYTe")
                        .HasColumnType("float");

                    b.Property<string>("ChucVu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("LuongCoBan")
                        .HasColumnType("float");

                    b.Property<double>("LuongThucTe")
                        .HasColumnType("float");

                    b.Property<string>("MaNhanVien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Nam")
                        .HasColumnType("int");

                    b.Property<int>("NgayCongChuNhat")
                        .HasColumnType("int");

                    b.Property<double>("PhuCap")
                        .HasColumnType("float");

                    b.Property<double>("PhuCapAnTrua")
                        .HasColumnType("float");

                    b.Property<double>("PhuCapDiLai")
                        .HasColumnType("float");

                    b.Property<double>("SoGioDiMuonVeSom")
                        .HasColumnType("float");

                    b.Property<double>("SoGioOT")
                        .HasColumnType("float");

                    b.Property<int>("Thang")
                        .HasColumnType("int");

                    b.Property<double>("ThucNhan")
                        .HasColumnType("float");

                    b.Property<double>("ThueTNCN")
                        .HasColumnType("float");

                    b.Property<double>("TienBaoHiem")
                        .HasColumnType("float");

                    b.Property<double>("TienCongCN")
                        .HasColumnType("float");

                    b.Property<double>("TienCongNgayLe")
                        .HasColumnType("float");

                    b.Property<double>("TienCongThuong")
                        .HasColumnType("float");

                    b.Property<double>("TienDiSomVeMuon")
                        .HasColumnType("float");

                    b.Property<double>("TienNgayNghiPhep")
                        .HasColumnType("float");

                    b.Property<double>("TienTangCa")
                        .HasColumnType("float");

                    b.Property<double>("TongKhauTru")
                        .HasColumnType("float");

                    b.Property<double>("TongLuong")
                        .HasColumnType("float");

                    b.Property<int>("TongSoNgayCong")
                        .HasColumnType("int");

                    b.Property<double>("UngLuong")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("BangLuongs");
                });

            modelBuilder.Entity("HRM.Domain.Models.BoPhan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TenBoPhan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BoPhans");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TenBoPhan = "Lập trình PC"
                        },
                        new
                        {
                            Id = 2,
                            TenBoPhan = "Cơ khí"
                        },
                        new
                        {
                            Id = 3,
                            TenBoPhan = "Kinh doanh"
                        },
                        new
                        {
                            Id = 4,
                            TenBoPhan = "Hành chính nhân sự"
                        },
                        new
                        {
                            Id = 5,
                            TenBoPhan = "Kế toán"
                        },
                        new
                        {
                            Id = 6,
                            TenBoPhan = "Kỹ thuật và hạ tầng"
                        });
                });

            modelBuilder.Entity("HRM.Domain.Models.ChucVu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double?>("PhuCapChucVu")
                        .HasColumnType("float");

                    b.Property<string>("TenChucVu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChucVus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PhuCapChucVu = 500000.0,
                            TenChucVu = "Nhân viên"
                        },
                        new
                        {
                            Id = 2,
                            PhuCapChucVu = 1000000.0,
                            TenChucVu = "Quản lý"
                        },
                        new
                        {
                            Id = 3,
                            PhuCapChucVu = 2000000.0,
                            TenChucVu = "Trưởng phòng"
                        },
                        new
                        {
                            Id = 4,
                            PhuCapChucVu = 5000000.0,
                            TenChucVu = "Trưởng bộ phận"
                        },
                        new
                        {
                            Id = 5,
                            PhuCapChucVu = 10000000.0,
                            TenChucVu = "Phó giám đốc"
                        },
                        new
                        {
                            Id = 6,
                            PhuCapChucVu = 15000000.0,
                            TenChucVu = "Giám đốc"
                        });
                });

            modelBuilder.Entity("HRM.Domain.Models.DanhMucKhenThuongKyLuat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CapKhenThuongKyLuat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HinhThucKhenThuongKyLuat")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DanhMucKhenThuongKyLuats");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CapKhenThuongKyLuat = "Cá nhân",
                            HinhThucKhenThuongKyLuat = "Thưởng tiền mặt"
                        },
                        new
                        {
                            Id = 2,
                            CapKhenThuongKyLuat = "Đội nhóm",
                            HinhThucKhenThuongKyLuat = "Thưởng tiền mặt"
                        },
                        new
                        {
                            Id = 3,
                            CapKhenThuongKyLuat = "Bộ phận",
                            HinhThucKhenThuongKyLuat = "Thưởng tiền mặt"
                        },
                        new
                        {
                            Id = 4,
                            CapKhenThuongKyLuat = "Cá nhân",
                            HinhThucKhenThuongKyLuat = "Trao giấy khen, bằng khen"
                        },
                        new
                        {
                            Id = 5,
                            CapKhenThuongKyLuat = "Đội nhóm",
                            HinhThucKhenThuongKyLuat = "Trao giấy khen, bằng khen"
                        },
                        new
                        {
                            Id = 6,
                            CapKhenThuongKyLuat = "Bộ phận",
                            HinhThucKhenThuongKyLuat = "Trao giấy khen, bằng khen"
                        },
                        new
                        {
                            Id = 7,
                            CapKhenThuongKyLuat = "Cá nhân",
                            HinhThucKhenThuongKyLuat = "Khiển trách bằng lời nói"
                        },
                        new
                        {
                            Id = 8,
                            CapKhenThuongKyLuat = "Đội nhóm",
                            HinhThucKhenThuongKyLuat = "Khiển trách bằng lời nói"
                        },
                        new
                        {
                            Id = 9,
                            CapKhenThuongKyLuat = "Bộ phận",
                            HinhThucKhenThuongKyLuat = "Khiển trách bằng lời nói"
                        },
                        new
                        {
                            Id = 10,
                            CapKhenThuongKyLuat = "Cá nhân",
                            HinhThucKhenThuongKyLuat = "Khiển trách bằng văn bản"
                        },
                        new
                        {
                            Id = 11,
                            CapKhenThuongKyLuat = "Đội nhóm",
                            HinhThucKhenThuongKyLuat = "Khiển trách bằng văn bản"
                        },
                        new
                        {
                            Id = 12,
                            CapKhenThuongKyLuat = "Bộ phận",
                            HinhThucKhenThuongKyLuat = "Khiển trách bằng văn bản"
                        },
                        new
                        {
                            Id = 13,
                            CapKhenThuongKyLuat = "Cá nhân",
                            HinhThucKhenThuongKyLuat = "Sa thải"
                        });
                });

            modelBuilder.Entity("HRM.Domain.Models.HopDong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("HeSoLuong")
                        .HasColumnType("real");

                    b.Property<string>("LoaiHopDong")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("LuongCoBan")
                        .HasColumnType("float");

                    b.Property<string>("MaNhanVien")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NhanSuId")
                        .HasColumnType("int");

                    b.Property<string>("SoHopDong")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NhanSuId");

                    b.ToTable("HopDongs");
                });

            modelBuilder.Entity("HRM.Domain.Models.KhenThuongKyLuat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CapKhenThuongKyLuat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaNhanVien")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayQuyetDinh")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NhanSuId")
                        .HasColumnType("int");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoQuyetDinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenHinhThuc")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NhanSuId");

                    b.ToTable("KhenThuongs");
                });

            modelBuilder.Entity("HRM.Domain.Models.KyLuat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CapKyLuat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoQuyetDinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThamQuyen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("ThoiGianBanHanh")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("KyLuats");
                });

            modelBuilder.Entity("HRM.Domain.Models.NhanSu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Anh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BoPhanId")
                        .HasColumnType("int");

                    b.Property<string>("CCCD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CapNgay")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ChucVuId")
                        .HasColumnType("int");

                    b.Property<string>("DanToc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<string>("HoTen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KetNapDang")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("LuongCoBan")
                        .HasColumnType("float");

                    b.Property<string>("MaNhanVien")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaSoBHXH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaSoThue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguyenQuan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiketNapDang")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("QuaTrinhCongTacId")
                        .HasColumnType("int");

                    b.Property<int?>("QuaTrinhDaoTaoId")
                        .HasColumnType("int");

                    b.Property<string>("STK")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoThich")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TonGiao")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BoPhanId");

                    b.HasIndex("ChucVuId");

                    b.HasIndex("QuaTrinhCongTacId")
                        .IsUnique()
                        .HasFilter("[QuaTrinhCongTacId] IS NOT NULL");

                    b.HasIndex("QuaTrinhDaoTaoId")
                        .IsUnique()
                        .HasFilter("[QuaTrinhDaoTaoId] IS NOT NULL");

                    b.ToTable("NhanSus");
                });

            modelBuilder.Entity("HRM.Domain.Models.NhanSuChucVu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChucVuId")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<int>("NhanSuId")
                        .HasColumnType("int");

                    b.Property<double>("PhuCapChucVu")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ChucVuId");

                    b.HasIndex("NhanSuId");

                    b.ToTable("NhanSuChucVus");
                });

            modelBuilder.Entity("HRM.Domain.Models.QuaTrinhCongTac", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ChucVu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaNhanVien")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiCongTac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TuNgayDenNgay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("QuaTrinhCongTacs");
                });

            modelBuilder.Entity("HRM.Domain.Models.QuaTrinhDaoTao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("HinhThucDaoTao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaNhanVien")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NganhHoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDaoTao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TuNgayDenNgay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VanBangChungChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("QuaTrinhDaoTaos");
                });

            modelBuilder.Entity("HRM.Domain.Models.QuanHeGiaDinh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ChucVu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DonViCongTac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoVaTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaNhanVien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoiQuanHe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NamSinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("NgheNghiep")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NhanSuId")
                        .HasColumnType("int");

                    b.Property<string>("NoiO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QueQuan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NhanSuId");

                    b.ToTable("QuanHeGiaDinhs");
                });

            modelBuilder.Entity("HRM.Domain.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Permission")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin",
                            Permission = ""
                        },
                        new
                        {
                            Id = 2,
                            Name = "Manager",
                            Permission = ""
                        },
                        new
                        {
                            Id = 3,
                            Name = "Compensation",
                            Permission = ""
                        },
                        new
                        {
                            Id = 4,
                            Name = "Staff",
                            Permission = ""
                        });
                });

            modelBuilder.Entity("HRM.Domain.Models.UngLuong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayUngLuong")
                        .HasColumnType("datetime2");

                    b.Property<int>("NhanSuId")
                        .HasColumnType("int");

                    b.Property<double?>("SoTienUng")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("NhanSuId");

                    b.ToTable("UngLuongs");
                });

            modelBuilder.Entity("HRM.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin",
                            Name = "admin",
                            Password = "??_?y??w+???l\0?_?n5?rY?????	?*",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            Email = "manager",
                            Name = "manager",
                            Password = "??_?y??w+???l\0?_?n5?rY?????	?*",
                            RoleId = 2
                        },
                        new
                        {
                            Id = 3,
                            Email = "compensation",
                            Name = "compensation",
                            Password = "??_?y??w+???l\0?_?n5?rY?????	?*",
                            RoleId = 3
                        },
                        new
                        {
                            Id = 4,
                            Email = "staff",
                            Name = "staff",
                            Password = "??_?y??w+???l\0?_?n5?rY?????	?*",
                            RoleId = 4
                        });
                });

            modelBuilder.Entity("HRM.Domain.Models.BangCong", b =>
                {
                    b.HasOne("HRM.Domain.Models.NhanSu", "NhanSu")
                        .WithMany()
                        .HasForeignKey("NhanSuId");

                    b.Navigation("NhanSu");
                });

            modelBuilder.Entity("HRM.Domain.Models.BangCongNhanSu", b =>
                {
                    b.HasOne("HRM.Domain.Models.BangCong", "BangCongs")
                        .WithMany()
                        .HasForeignKey("BangCongId");

                    b.Navigation("BangCongs");
                });

            modelBuilder.Entity("HRM.Domain.Models.HopDong", b =>
                {
                    b.HasOne("HRM.Domain.Models.NhanSu", "NhanSu")
                        .WithMany()
                        .HasForeignKey("NhanSuId");

                    b.Navigation("NhanSu");
                });

            modelBuilder.Entity("HRM.Domain.Models.KhenThuongKyLuat", b =>
                {
                    b.HasOne("HRM.Domain.Models.NhanSu", "NhanSu")
                        .WithMany()
                        .HasForeignKey("NhanSuId");

                    b.Navigation("NhanSu");
                });

            modelBuilder.Entity("HRM.Domain.Models.NhanSu", b =>
                {
                    b.HasOne("HRM.Domain.Models.BoPhan", "BoPhan")
                        .WithMany()
                        .HasForeignKey("BoPhanId");

                    b.HasOne("HRM.Domain.Models.ChucVu", "ChucVu")
                        .WithMany()
                        .HasForeignKey("ChucVuId");

                    b.HasOne("HRM.Domain.Models.QuaTrinhCongTac", "QuaTrinhCongTac")
                        .WithOne("NhanSu")
                        .HasForeignKey("HRM.Domain.Models.NhanSu", "QuaTrinhCongTacId");

                    b.HasOne("HRM.Domain.Models.QuaTrinhDaoTao", "QuaTrinhDaoTao")
                        .WithOne("NhanSu")
                        .HasForeignKey("HRM.Domain.Models.NhanSu", "QuaTrinhDaoTaoId");

                    b.Navigation("BoPhan");

                    b.Navigation("ChucVu");

                    b.Navigation("QuaTrinhCongTac");

                    b.Navigation("QuaTrinhDaoTao");
                });

            modelBuilder.Entity("HRM.Domain.Models.NhanSuChucVu", b =>
                {
                    b.HasOne("HRM.Domain.Models.ChucVu", "ChucVu")
                        .WithMany()
                        .HasForeignKey("ChucVuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRM.Domain.Models.NhanSu", "NhanSu")
                        .WithMany()
                        .HasForeignKey("NhanSuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChucVu");

                    b.Navigation("NhanSu");
                });

            modelBuilder.Entity("HRM.Domain.Models.QuanHeGiaDinh", b =>
                {
                    b.HasOne("HRM.Domain.Models.NhanSu", "NhanSu")
                        .WithMany()
                        .HasForeignKey("NhanSuId");

                    b.Navigation("NhanSu");
                });

            modelBuilder.Entity("HRM.Domain.Models.UngLuong", b =>
                {
                    b.HasOne("HRM.Domain.Models.NhanSu", "NhanSu")
                        .WithMany()
                        .HasForeignKey("NhanSuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NhanSu");
                });

            modelBuilder.Entity("HRM.Domain.Models.QuaTrinhCongTac", b =>
                {
                    b.Navigation("NhanSu")
                        .IsRequired();
                });

            modelBuilder.Entity("HRM.Domain.Models.QuaTrinhDaoTao", b =>
                {
                    b.Navigation("NhanSu")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
