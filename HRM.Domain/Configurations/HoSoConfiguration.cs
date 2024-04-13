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
    public class HoSoConfiguration : IEntityTypeConfiguration<HoSo>
    {
        public void Configure(EntityTypeBuilder<HoSo> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
