using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Configurations;
using HRM.Domain.Models;

namespace HRM.EFCore
{
    public class HRMDbContext : DbContext
    {
        public DbSet<BangCong> BangCongs { get; set; }
        public DbSet<BangLuong> BangLuongs { get; set; }
        public DbSet<BoPhan> BoPhans { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<HopDong> HopDongs { get; set; }
        public DbSet<HoSo> HoSos { get; set; }
        public DbSet<KhoanKhauTru> KhoanKhauTrus { get; set; }
        public DbSet<KhoanThu> KhoanThus { get; set; }
        public DbSet<NhanSu> NhanSus { get; set; }
        public DbSet<PhucLoiVaPhuCap> PhucLoiVaPhuCaps { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<QuanHeGiaDinh> QuanHeGiaDinhs { get; set; }
        public HRMDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BangCongConfiguration());
            modelBuilder.ApplyConfiguration(new BangLuongConfiguration());
            modelBuilder.ApplyConfiguration(new BoPhanConfiguration());
            modelBuilder.ApplyConfiguration(new ChucVuConfiguration());
            modelBuilder.ApplyConfiguration(new HopDongConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new HopDongConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new HoSoConfiguration());
            modelBuilder.ApplyConfiguration(new KhoanKhauTruConfiguration());
            modelBuilder.ApplyConfiguration(new KhoanThuConfiguration());
            modelBuilder.ApplyConfiguration(new NhanSuConfiguration());
            modelBuilder.ApplyConfiguration(new PhucLoiVaPhuCapConfiguration());
            modelBuilder.ApplyConfiguration(new ViTriConfiguration());
            modelBuilder.ApplyConfiguration(new QuanHeGiaDinhConfiguration());
        }
    }
}
