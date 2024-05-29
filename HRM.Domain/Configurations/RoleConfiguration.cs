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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(new Role()
            {
                 Id = 1,
                 Name = "Admin",
                 Permission = ""
            },
            new Role()
            {
                Id = 2,
                Name = "Manager",
                Permission = ""
            },
            new Role()
            {
                Id = 3,
                Name = "Compensation",
                Permission = ""
            },
            new Role()
            {
                Id = 4,
                Name = "Staff",
                Permission = ""
            });
        }
    }
}
