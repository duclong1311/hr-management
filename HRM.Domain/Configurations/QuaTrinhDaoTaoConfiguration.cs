using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRM.Domain.Configurations
{
    public class QuaTrinhDaoTaoConfiguration : IEntityTypeConfiguration<QuaTrinhDaoTao>
    {
        public void Configure(EntityTypeBuilder<QuaTrinhDaoTao> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
