using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class HopDong : IEntity
    {
        public string? MaNhanVien { get; set; }
        public int? NhanSuId { get; set; }
        public string? SoHopDong { get; set;}
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? LoaiHopDong { get; set; }
        public double? LuongCoBan { get; set; }
        public float? HeSoLuong { get; set; }
        public NhanSu? NhanSu { get; set; }
    }
}
