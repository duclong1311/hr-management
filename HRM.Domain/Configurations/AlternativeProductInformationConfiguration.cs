using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Models;

namespace HRM.Domain.Configurations
{
    public class AlternativeProductInformationConfiguration : IEntityTypeConfiguration<AlternativeProductInformation>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AlternativeProductInformation> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
