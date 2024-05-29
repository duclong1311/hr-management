using HRM.Domain.Models;
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
                Email = "admin",
                Password = CryptographyUtil.SHA256Hash("111111"),
                RoleId = 1
            }, new User()
            {
                Id = 2,
                Name = "manager",
                Email = "manager",
                Password = CryptographyUtil.SHA256Hash("111111"),
                RoleId = 2
            },
            new User()
            {
                Id = 3,
                Name = "compensation",
                Email = "compensation",
                Password = CryptographyUtil.SHA256Hash("111111"),
                RoleId = 3
            },
            new User()
            {
                Id = 4,
                Name = "staff",
                Email = "staff",
                Password = CryptographyUtil.SHA256Hash("111111"),
                RoleId = 4
            });
        }
    }
}
