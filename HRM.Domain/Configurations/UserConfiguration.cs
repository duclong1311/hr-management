using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Domain.Models;
using System.Xml.Linq;
using Terp.Domain.Utils;

namespace Terp.Domain.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(new User()
            {
                Id = Guid.NewGuid(),
                Name = "admin",
                Email = "admin@gmail.com",
                Password = CryptographyUtil.SHA256Hash("123456"),
                FingerprintCode = ""
            });
        }
        
    }
}
