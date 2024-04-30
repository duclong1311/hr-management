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
        public string? CapKhenThuong { get; set; }
        public string? TenHinhThucKhenThuong { get; set; }
        public string? CapKyLuat { get; set; }
        public string? TenHinhThucKyLuat { get; set; }
        public DateTime NgayQuyetDinh { get; set; }
        public string SoQuyetDinh { get; set; }
        public NhanSu? NhanSu { get; set; }  
    }
}
