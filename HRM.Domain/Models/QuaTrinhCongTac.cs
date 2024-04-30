using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Entities;

namespace HRM.Domain.Models
{
    public class QuaTrinhCongTac : IEntity
    {
        public string TuNgayDenNgay { get; set; }
        public string NoiCongTac { get; set; }
        public string ChucVu { get; set; }
        public NhanSu NhanSu { get; set; }
    }
}
