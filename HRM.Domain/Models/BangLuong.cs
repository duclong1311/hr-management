using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class BangLuong : IEntity
    {
        public string MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public string ChucVu { get; set; }//
        public double? LuongCoBan { get; set; }//
        public int TongSoNgayCong { get; set; }//
        public double TienCongThuong { get; set; }
        public double LuongThucTe { get; set; }//
        public int NgayCongChuNhat { get; set; }//
        public double TienCongCN { get; set; }
        public double SoGioOT { get; set; }//
        public double PhuCapAnTrua { get; set; }//
        public double PhuCapDiLai { get; set; }//
        public double PhuCap { get; set; }
        public double TongLuong { get; set; }//
        public double BaoHiemXaHoi { get; set; }//
        public double BaoHiemYTe { get; set; }//
        public double SoGioDiMuonVeSom { get; set; }//
        public double TienCongNgayLe { get; set; }
        public double TienTangCa { get; set; }
        public double TienNgayNghiPhep { get; set; }
        public double TienDiSomVeMuon { get; set; }
        public double TienBaoHiem { get; set; }
        public double UngLuong { get; set; }
        public double TongKhauTru { get; set; }//
        public double ThucNhan { get; set; }
        public double ThueTNCN { get; set; }

    }
}
