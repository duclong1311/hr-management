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
        public int? BangCongId { get; set; }
        public NhanSu MaNhanVien { get; set; }
        public NhanSu HoTen { get; set; }
        public double SoTienUng { get; set; }
        public DateTime NgayUngLuong{ get; set;}
        public string GhiChu { get; set; }
    }
}
