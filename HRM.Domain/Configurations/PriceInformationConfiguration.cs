using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Domain.Models;

namespace Terp.Domain.Configurations
{
    public class PriceInformationConfiguration : IEntityTypeConfiguration<PriceInformation>
    {
        public void Configure(EntityTypeBuilder<PriceInformation> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
