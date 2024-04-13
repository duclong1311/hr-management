using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Core.Entities;

namespace Terp.Domain.Models
{
    public class LogInformation : IEntity
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateTime {  get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
} 
