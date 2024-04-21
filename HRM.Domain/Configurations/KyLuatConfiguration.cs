using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HRM.Domain.Configurations
{
    public class KyLuatConfiguration : IEntityTypeConfiguration<KyLuat>
    {
        public void Configure(EntityTypeBuilder<KyLuat> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
