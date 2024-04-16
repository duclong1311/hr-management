using HRM.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Configurations
{
    public class BoPhanConfiguration : IEntityTypeConfiguration<BoPhan>
    {
        public void Configure(EntityTypeBuilder<BoPhan> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(new BoPhan()
            {
                Id = 1,
                TenBoPhan = "Lập trình PC",
            }, new BoPhan()
            {
                Id = 2,
                TenBoPhan = "Cơ khí",
            }, new BoPhan()
            {
                Id = 3,
                TenBoPhan = "Kinh doanh",
            }, new BoPhan()
            {
                Id = 4,
                TenBoPhan = "Hành chính nhân sự",
            }, new BoPhan()
            {
                Id = 5,
                TenBoPhan = "Kế toán",
            }, new BoPhan()
            {
                Id = 6,
                TenBoPhan = "Kỹ thuật và hạ tầng",
            });
        }
    }
}
