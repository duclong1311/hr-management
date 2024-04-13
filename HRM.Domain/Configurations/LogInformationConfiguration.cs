using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Domain.Models;

namespace Terp.Domain.Configurations
{
    public class LogInformationConfiguration : IEntityTypeConfiguration<LogInformation>
    {
        public void Configure(EntityTypeBuilder<LogInformation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
