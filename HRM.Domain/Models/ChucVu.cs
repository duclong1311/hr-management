using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class ChucVu : IEntity
    {
        public string TenChucVu {  get; set; }
        public double PhuCapChucVu { get; set;}
    }
}
