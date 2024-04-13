using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Core.Entities;

namespace Terp.Domain.Models
{
    public class Currency : IEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double ExchangeRate { get; set; }
        public bool IsDelete { get; set; }
    }
}
