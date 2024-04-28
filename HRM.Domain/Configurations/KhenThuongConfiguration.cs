using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Entities;
using HRM.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRM.Domain.Configurations
{
    public class KhenThuongConfiguration : IEntityTypeConfiguration<KhenThuongKyLuat>
    {
        public void Configure(EntityTypeBuilder<KhenThuongKyLuat> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
