using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Entities;

namespace HRM.Domain.Models
{
    public class KhenThuongKyLuat : IEntity
    {
        public string? MaNhanVien { get; set; }
        public int? NhanSuId { get; set; }
        public string? CapKhenThuongKyLuat { get; set; }
        public string? TenHinhThuc { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public string? SoQuyetDinh { get; set; }
        public string? NoiDung { get; set; }
        public NhanSu? NhanSu { get; set; }  
    }
}
