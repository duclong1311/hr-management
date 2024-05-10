using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Entities;

namespace HRM.Domain.Models
{
    public class BangCongNhanSu : IEntity
    {
        public string? MaNhanVien { get; set; }
        public string? HoTen { get; set; }
        public DateTime? Ngay { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public DateTime? GioVao { get; set; }
        public DateTime? GioRa { get; set; }
    }
}
