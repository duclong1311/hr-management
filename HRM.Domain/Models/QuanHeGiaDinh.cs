using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Entities;

namespace HRM.Domain.Models
{
    public class QuanHeGiaDinh : IEntity
    {
        public NhanSu MaNhanVien { get; set; }
        public string MoiQuanHe { get; set; }
        public string HoVaTen { get; set; } 
        public DateTime NamSinh { get; set; }
        public string QueQuan { get; set; }
        public string NgheNghiep { get; set; }  
        public string DonViCongTac { get; set; }
        public string NoiO { get; set; }
        public string ChucVu { get; set; }  
    }
}
