using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Domain.Models;

namespace Terp.Domain.Configurations
{
    public class AlternativeProductInformationConfiguration : IEntityTypeConfiguration<AlternativeProductInformation>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AlternativeProductInformation> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
