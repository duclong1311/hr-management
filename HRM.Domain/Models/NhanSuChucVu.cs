using HRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class NhanSuChucVu : IEntity
    {

        public int NhanSuId { get; set; }
        public NhanSu NhanSu { get; set; }
        public int ChucVuId { get; set; }
        public ChucVu ChucVu { get; set; }
        public double PhuCapChucVu { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }
}
