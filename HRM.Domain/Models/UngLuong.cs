using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class UngLuong : IEntity
    {
        public string? MaNhanVien { get; set; }
        public string? HoTen { get; set; }
        public double? SoTienUng { get; set; }
        public DateTime? NgayUngLuong{ get; set;}
        public string? GhiChu { get; set; }
        public NhanSu? NhanSu { get; set; }
    }
}
