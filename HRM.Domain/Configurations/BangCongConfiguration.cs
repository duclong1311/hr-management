﻿using HRM.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Configurations
{
    public class BangCongConfiguration : IEntityTypeConfiguration<BangCong>
    {
        public void Configure(EntityTypeBuilder<BangCong> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
