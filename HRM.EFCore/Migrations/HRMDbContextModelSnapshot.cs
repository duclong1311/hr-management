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

                    b.Property<float>("DiMuonVeSom")
                        .HasColumnType("real");

                    b.Property<int>("Nam")
                        .HasColumnType("int");

                    b.Property<int>("NgayNghiPhep")
                        .HasColumnType("int");

                    b.Property<int?>("NhanSuId")
                        .HasColumnType("int");

                    b.Property<int>("Thang")
                        .HasColumnType("int");

                    b.Property<bool>("ThuongChuyenCan")
                        .HasColumnType("bit");

                    b.Property<int>("TongSoNgayCong")
                        .HasColumnType("int");

                    b.Property<int>("TongSoNgayCongCN")
                        .HasColumnType("int");

                    b.Property<int>("TongSoNgayCongNgayLe")
                        .HasColumnType("int");

                    b.Property<double>("TongTimeOT")
                        .HasColumnType("float");

                    b.Property<double>("UngLuong")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("NhanSuId");

                    b.ToTable("BangCongs");
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

                    b.Property<DateTime>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<double>("PhuCapChucVu")
                        .HasColumnType("float");

                    b.Property<string>("TenChucVu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChucVus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NgayBatDau = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NgayKetThuc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhuCapChucVu = 0.0,
                            TenChucVu = "Nhân viên"
                        },
                        new
                        {
                            Id = 2,
                            NgayBatDau = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NgayKetThuc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhuCapChucVu = 0.0,
                            TenChucVu = "Quản lý"
                        },
                        new
                        {
                            Id = 3,
                            NgayBatDau = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NgayKetThuc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhuCapChucVu = 0.0,
                            TenChucVu = "Trưởng phòng"
                        },
                        new
                        {
                            Id = 4,
                            NgayBatDau = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NgayKetThuc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhuCapChucVu = 0.0,
                            TenChucVu = "Trưởng bộ phận"
                        },
                        new
                        {
                            Id = 5,
                            NgayBatDau = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NgayKetThuc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhuCapChucVu = 0.0,
                            TenChucVu = "Giám đốc"
                        });
                });

            modelBuilder.Entity("HRM.Domain.Models.HopDong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ChiTietHopDong")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoaiHopDong")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NhanSuId")
                        .HasColumnType("int");

                    b.Property<string>("SoHopDong")
                        .IsRequired()
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

                    b.Property<string>("CapKhenThuong")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CapKyLuat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayQuyetDinh")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NhanSuId")
                        .HasColumnType("int");

                    b.Property<string>("SoQuyetDinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenHinhThucKhenThuong")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenHinhThucKyLuat")
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

                    b.ToTable("NhanSus");
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

                    b.Property<int>("NhanSuId")
                        .HasColumnType("int");

                    b.Property<int>("NhanSuId1")
                        .HasColumnType("int");

                    b.Property<string>("NoiCongTac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TuNgayDenNgay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NhanSuId1");

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

                    b.Property<string>("NganhHoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NhanSuId")
                        .HasColumnType("int");

                    b.Property<int>("NhanSuId1")
                        .HasColumnType("int");

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

                    b.HasIndex("NhanSuId1");

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

                    b.Property<string>("MoiQuanHe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NamSinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("NgheNghiep")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NhanSuId")
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
                });

            modelBuilder.Entity("HRM.Domain.Models.UngLuong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayUngLuong")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NhanSuId")
                        .HasColumnType("int");

                    b.Property<double>("SoTienUng")
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

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "1",
                            Name = "admin",
                            Password = "??_?y??w+???l\0?_?n5?rY?????	?*"
                        });
                });

            modelBuilder.Entity("HRM.Domain.Models.BangCong", b =>
                {
                    b.HasOne("HRM.Domain.Models.NhanSu", "NhanSu")
                        .WithMany()
                        .HasForeignKey("NhanSuId");

                    b.Navigation("NhanSu");
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

                    b.Navigation("BoPhan");

                    b.Navigation("ChucVu");
                });

            modelBuilder.Entity("HRM.Domain.Models.QuaTrinhCongTac", b =>
                {
                    b.HasOne("HRM.Domain.Models.NhanSu", "NhanSu")
                        .WithMany()
                        .HasForeignKey("NhanSuId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NhanSu");
                });

            modelBuilder.Entity("HRM.Domain.Models.QuaTrinhDaoTao", b =>
                {
                    b.HasOne("HRM.Domain.Models.NhanSu", "NhanSu")
                        .WithMany()
                        .HasForeignKey("NhanSuId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NhanSu");
                });

            modelBuilder.Entity("HRM.Domain.Models.QuanHeGiaDinh", b =>
                {
                    b.HasOne("HRM.Domain.Models.NhanSu", "NhanSu")
                        .WithMany()
                        .HasForeignKey("NhanSuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NhanSu");
                });

            modelBuilder.Entity("HRM.Domain.Models.UngLuong", b =>
                {
                    b.HasOne("HRM.Domain.Models.NhanSu", "NhanSu")
                        .WithMany()
                        .HasForeignKey("NhanSuId");

                    b.Navigation("NhanSu");
                });
#pragma warning restore 612, 618
        }
    }
}
