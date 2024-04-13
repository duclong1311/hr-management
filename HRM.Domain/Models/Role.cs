using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Core.Entities;

namespace Terp.Domain.Models
{
    public class Role : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Permission { get; set; }
    }
}
