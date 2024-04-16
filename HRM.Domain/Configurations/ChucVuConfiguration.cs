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
                TenChucVu = "Nhân viên"
            }, new ChucVu()
            {
                Id = 2,
                TenChucVu = "Quản lý"
            }, new ChucVu()
            {
                Id = 3,
                TenChucVu = "Trưởng phòng"
            }, new ChucVu()
            {
                Id = 4,
                TenChucVu = "Trưởng bộ phận"
            }, new ChucVu()
            {
                Id = 5,
                TenChucVu = "Giám đốc"
            });
        }
    }
}
