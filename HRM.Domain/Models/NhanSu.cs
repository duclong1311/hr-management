using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class NhanSu : IEntity
    {
        //Sơ yếu lý lịch
        public string? MaNhanVien { get; set; }
        public string? Anh { get; set; }
        public string? HoTen {  get; set; }
        public DateTime? NgaySinh { get; set; }
        public bool? GioiTinh { get; set; }
        public string? NguyenQuan { get; set; }
        public string? DanToc { get; set; }
        public string? TonGiao { get; set; }
        public string? CCCD { get; set; }
        public DateTime CapNgay { get; set; }
        public string? KetNapDang { get; set; }
        public string? NoiketNapDang { get; set; }
        public string? SoThich { get; set; }

        //Thông tin nhận lương
        public string? STK { get; set; }
        public string? MaSoBHXH {  get; set; }
        public string? MaSoThue {  get; set; }

        //Thông tin để tính lương
        public double? LuongCoBan {  get; set; }
        //public double? PhuCapDiLai { get; set; }
        //public double? PhuCapAnTrua { get; set; }    
        //public double? PhucLoi { get; set; } 

        //...
        public int? BoPhanId {  get; set; }
        public BoPhan? BoPhan { get; set; }
        public int? ChucVuId {  get; set; }
        public ChucVu? ChucVu { get; set; }
        public int? QuaTrinhDaoTaoId { get; set; }
        public int? QuaTrinhCongTacId { get; set; }
    }
}
