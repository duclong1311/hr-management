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
        }
    }
}
