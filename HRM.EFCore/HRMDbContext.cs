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
        public DbSet<BoPhan> BoPhans { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<HopDong> HopDongs { get; set; }
        public DbSet<KhenThuong> KhenThuongs { get; set; }
        public DbSet<KyLuat> KyLuats { get; set; }
        public DbSet<NhanSu> NhanSus { get; set; }
        public DbSet<QuanHeGiaDinh> QuanHeGiaDinhs { get; set; }
        public DbSet<QuaTrinhCongTac> QuaTrinhCongTacs { get; set; }
        public DbSet<QuaTrinhDaoTao> QuaTrinhDaoTaos { get; set; }
        public DbSet<UngLuong> UngLuongs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public HRMDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BangCongConfiguration());
            modelBuilder.ApplyConfiguration(new BoPhanConfiguration());
            modelBuilder.ApplyConfiguration(new ChucVuConfiguration());
            modelBuilder.ApplyConfiguration(new HopDongConfiguration());
            modelBuilder.ApplyConfiguration(new KhenThuongConfiguration());
            modelBuilder.ApplyConfiguration(new KyLuatConfiguration());
            modelBuilder.ApplyConfiguration(new NhanSuConfiguration());
            modelBuilder.ApplyConfiguration(new QuanHeGiaDinhConfiguration());
            modelBuilder.ApplyConfiguration(new QuaTrinhCongTacConfiguration());
            modelBuilder.ApplyConfiguration(new QuaTrinhDaoTaoConfiguration());
            modelBuilder.ApplyConfiguration(new UngLuongConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
