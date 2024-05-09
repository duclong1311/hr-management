using HRM.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Configurations
{
    public class ChucVuConfiguration : IEntityTypeConfiguration<ChucVu>
    {
        public void Configure(EntityTypeBuilder<ChucVu> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(new ChucVu()
            {
                Id = 1,
                TenChucVu = "Nhân viên",
                PhuCapChucVu = 500000
            }, new ChucVu()
            {
                Id = 2,
                TenChucVu = "Quản lý",
                PhuCapChucVu = 1000000
            }, new ChucVu()
            {
                Id = 3,
                TenChucVu = "Trưởng phòng",
                PhuCapChucVu = 2000000
            }, new ChucVu()
            {
                Id = 4,
                TenChucVu = "Trưởng bộ phận",
                PhuCapChucVu = 5000000
            }, new ChucVu()
            {
                Id = 5,
                TenChucVu = "Phó giám đốc",
                PhuCapChucVu = 10000000
            },new ChucVu()
            {
                Id = 6,
                TenChucVu = "Giám đốc",
                PhuCapChucVu = 15000000
            });
        }
    }
}
