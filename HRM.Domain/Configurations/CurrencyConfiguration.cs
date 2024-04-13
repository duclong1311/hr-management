using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Terp.Domain.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => !x.IsDelete);
            builder.HasData(new Currency()
            {
                Id = Guid.NewGuid(),
                Code = "USD",
                Name = "US Dollar",
                ExchangeRate = 1
            }, new Currency()
            {
                Id = Guid.NewGuid(),
                Code = "KRW",
                Name = "Won",
                ExchangeRate = 1300,
            }, new Currency()
            {
                Id = Guid.NewGuid(),
                Code = "VND",
                Name = "Đồng",
                ExchangeRate = 24000
            }, new Currency()
            {
                Id = Guid.NewGuid(),
                Code = "EUR",
                Name = "Euro",
                ExchangeRate = 0.91
            },new Currency()
            {
                Id = Guid.NewGuid(),
                Code = "JPY",
                Name = "Yên",
                ExchangeRate = 20
            }, new Currency()
            {
                Id = Guid.NewGuid(),
                Code = "CNY",
                Name = "Nhân dân tệ",
                ExchangeRate = 50
            });

        }
    }
}
