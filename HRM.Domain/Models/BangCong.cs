using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class BangCong : IEntity
    {
        public double TongTimeOT { get; set; }
        public int TongSoNgayCong {  get; set; }
        public int NgayNghiPhep { get; set; }
        public int NgayKhongNghiPhep { get; set; }
    }
}
