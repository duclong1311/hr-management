﻿using HRM.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Domain.Utils;

namespace HRM.Domain.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(new User()
            {
                Id = 1,
                Name = "admin",
                Email = "1",
                Password = CryptographyUtil.SHA256Hash("111111"),
            });
        }
    }
}
