using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Core.Entities;

namespace Terp.Domain.Models
{
    public class PriceInformation : IEntity
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ProviderId { get; set; }
        public Guid? CurrencyId { get; set; }
        public DateTime BidDate { get; set; }
        public double Price { get; set; }
        public virtual Product Product { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Provider Provider { get; set; }
    }
}
