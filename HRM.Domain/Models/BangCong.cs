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
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaNhanVien { get; set; }
        public string HoTen {  get; set; }
        public double TongTimeOT { get; set; }
        public int TongSoNgayCong {  get; set; }
        public int TongSoNgayCongCN {  get; set; }
        public int TongSoNgayCongNgayLe { get; set; }
        public float DiMuonVeSom { get; set; }
        public bool ThuongChuyenCan { get; set; }   
        public int NgayNghiPhep { get; set; }
        public double UngLuong { get; set; }
    }
}
