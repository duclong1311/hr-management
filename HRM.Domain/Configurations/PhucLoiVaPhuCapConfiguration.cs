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
    public class PhucLoiVaPhuCapConfiguration : IEntityTypeConfiguration<PhucLoiVaPhuCap>
    {
        public void Configure(EntityTypeBuilder<PhucLoiVaPhuCap> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
