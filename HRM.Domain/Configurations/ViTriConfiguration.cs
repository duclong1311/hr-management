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
    public class ViTriConfiguration : IEntityTypeConfiguration<ViTri>
    {
        public void Configure(EntityTypeBuilder<ViTri> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(new ViTri()
            {
                Id = 1,
                TenViTri = "VT1"
            }, new ViTri()
            {
                Id = 2,
                TenViTri = "VT2"
            }, new ViTri()
            {
                Id = 3,
                TenViTri = "VT3"
            }, new ViTri()
            {
                Id = 4,
                TenViTri = "VT4"
            });
        }
    }
}
