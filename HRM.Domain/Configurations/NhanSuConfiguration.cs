using HRM.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Core.Entities;

namespace HRM.Domain.Configurations
{
    public class NhanSuConfiguration : IEntityTypeConfiguration<NhanSu>
    {
        public void Configure(EntityTypeBuilder<NhanSu> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
