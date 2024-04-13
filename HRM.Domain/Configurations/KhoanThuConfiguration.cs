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
    public class KhoanThuConfiguration : IEntityTypeConfiguration<KhoanThu>
    {
        public void Configure(EntityTypeBuilder<KhoanThu> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
