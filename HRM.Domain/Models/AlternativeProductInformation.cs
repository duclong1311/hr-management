using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Core.Entities;

namespace Terp.Domain.Models
{
    public class AlternativeProductInformation : IEntity
    {
        public Guid Id { get; set; }
        public string OriginalProductCode { get; set; }
        public string AlternativeProductCode { get; set; }
    }
}
