using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Terp.Core.Entities;

namespace Terp.Domain.Models
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UnitId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
